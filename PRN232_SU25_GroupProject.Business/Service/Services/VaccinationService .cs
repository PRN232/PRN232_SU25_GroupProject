using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class VaccinationService : IVaccinationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VaccinationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<VaccinationCampaignDto> CreateCampaignAsync(CreateVaccinationCampaignRequest request)
        {
            var campaign = _mapper.Map<VaccinationCampaign>(request);
            campaign.Status = VaccinationStatus.Planned;

            await _unitOfWork.VaccinationCampaignRepository.AddAsync(campaign);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<VaccinationCampaignDto>(campaign);
        }

        public async Task<List<VaccinationCampaignDto>> GetActiveCampaignsAsync()
        {
            var campaigns = await _unitOfWork.VaccinationCampaignRepository
                .Query()
                .Where(c => c.ScheduledDate >= DateTime.Today)
                .ToListAsync();

            var campaignIds = campaigns.Select(c => c.Id).ToList();

            var consents = await _unitOfWork.GetRepository<VaccinationConsent>().Query()
                .Where(c => campaignIds.Contains(c.CampaignId))
                .ToListAsync();

            var records = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => campaignIds.Contains(r.CampaignId))
                .ToListAsync();

            var campaignDtos = campaigns.Select(c =>
            {
                var dto = _mapper.Map<VaccinationCampaignDto>(c);
                dto.ConsentReceived = consents.Count(x => x.CampaignId == c.Id && x.ConsentGiven);
                dto.VaccinationsCompleted = records.Count(x => x.CampaignId == c.Id && x.Result == VaccinationResult.Completed);
                // Optional: Set TotalStudents via eligible students
                return dto;
            }).ToList();

            return campaignDtos;
        }

        public async Task<bool> SendConsentFormsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return false;

            var targetGrades = campaign.TargetGrades.Split(',').Select(g => g.Trim()).ToList();

            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => targetGrades.Contains(s.ClassName))
                .ToListAsync();

            foreach (var student in students)
            {
                var exists = await _unitOfWork.GetRepository<VaccinationConsent>().Query()
                    .AnyAsync(c => c.CampaignId == campaignId && c.StudentId == student.Id);

                if (!exists)
                {
                    var consent = new VaccinationConsent
                    {
                        CampaignId = campaignId,
                        StudentId = student.Id,
                        ParentId = student.ParentId,
                        ConsentGiven = false,
                        ConsentDate = DateTime.Now
                    };
                    await _unitOfWork.GetRepository<VaccinationConsent>().AddAsync(consent);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SubmitConsentAsync(SubmitConsentRequest request)
        {
            var consentRepo = _unitOfWork.GetRepository<VaccinationConsent>();

            var consent = await consentRepo.Query()
                .FirstOrDefaultAsync(c => c.CampaignId == request.CampaignId && c.StudentId == request.StudentId);

            if (consent == null) return false;

            consent.ConsentGiven = request.ConsentGiven;
            consent.ParentSignature = request.ParentSignature;
            consent.ConsentDate = request.ConsentDate;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<StudentDto>> GetEligibleStudentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return new();

            var targetGrades = campaign.TargetGrades.Split(',').Select(g => g.Trim()).ToList();

            var students = await _unitOfWork.StudentRepository.Query()
                .Where(s => targetGrades.Contains(s.ClassName))
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<VaccinationRecordDto> RecordVaccinationAsync(RecordVaccinationRequest request)
        {
            var entity = _mapper.Map<VaccinationRecord>(request);
            entity.VaccinationDate = request.VaccinationDate;

            await _unitOfWork.VaccinationRecordRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Enrich DTO
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(request.StudentId);
            var nurse = await _unitOfWork.GetRepository<SchoolNurse>().GetByIdAsync(request.NurseId);
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(request.CampaignId);

            var dto = _mapper.Map<VaccinationRecordDto>(entity);
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.NurseName = nurse?.FullName;
            dto.CampaignName = campaign?.CampaignName;

            return dto;
        }

        public async Task<List<VaccinationRecordDto>> GetStudentVaccinationHistoryAsync(int studentId)
        {
            var records = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            var campaignIds = records.Select(r => r.CampaignId).Distinct().ToList();
            var nurseIds = records.Select(r => r.NurseId).Distinct().ToList();

            var campaigns = await _unitOfWork.VaccinationCampaignRepository.Query()
                .Where(c => campaignIds.Contains(c.Id))
                .ToDictionaryAsync(c => c.Id);

            var nurses = await _unitOfWork.GetRepository<SchoolNurse>().Query()
                .Where(n => nurseIds.Contains(n.Id))
                .ToDictionaryAsync(n => n.Id);

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);

            return records.Select(r =>
            {
                var dto = _mapper.Map<VaccinationRecordDto>(r);
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.CampaignName = campaigns.TryGetValue(r.CampaignId, out var camp) ? camp.CampaignName : null;
                dto.NurseName = nurses.TryGetValue(r.NurseId, out var nurse) ? nurse.FullName : null;
                return dto;
            }).ToList();
        }
    }

}
