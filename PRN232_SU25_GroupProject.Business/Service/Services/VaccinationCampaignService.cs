using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class VaccinationCampaignService : IVaccinationCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VaccinationCampaignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<VaccinationCampaignDto>>> GetAllCampaignsAsync()
        {
            var campaigns = await _unitOfWork.VaccinationCampaignRepository.GetAllAsync();
            var vaccinationRecords = await _unitOfWork.VaccinationRecordRepository.GetAllAsync();
            var healthConsents = await _unitOfWork.MedicalConsentRepository.Query()
                .Where(s => s.ConsentType == DataAccess.Enums.ConsentType.Vaccine)
                .ToListAsync();

            var trueConsents = healthConsents.Where(s => s.ConsentGiven == true);

            var dtos = campaigns.Select(c =>
            {
                var totalStudents = healthConsents.Count(s => s.CampaignId == c.Id);
                var campaignRecords = vaccinationRecords.Where(r => r.CampaignId == c.Id).ToList();
                var completed = campaignRecords.Count;


                return new VaccinationCampaignDto
                {
                    Id = c.Id,
                    CampaignName = c.CampaignName,
                    VaccineType = c.VaccineType,
                    ScheduledDate = c.ScheduledDate,
                    TargetGrades = c.TargetGrades,
                    Status = c.Status,
                    ConsentReceived = trueConsents.Count(s => s.CampaignId == c.Id),
                    TotalStudents = totalStudents,
                    VaccinationsCompleted = completed,

                };
            }).ToList();

            return ApiResponse<List<VaccinationCampaignDto>>.SuccessResult(dtos, "Lấy danh sách chiến dịch tiêm chủng thành công.");
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<VaccinationCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");

            var vaccinationRecords = await _unitOfWork.VaccinationRecordRepository.GetAllAsync();
            var vaccineConsent = await _unitOfWork.MedicalConsentRepository.Query()
                .Where(s => s.ConsentType == DataAccess.Enums.ConsentType.Vaccine)
                .ToListAsync();
            var trueConsents = vaccineConsent.Where(s => s.ConsentGiven == true && s.CampaignId == campaign.Id).Count();
            var totalStudents = vaccineConsent.Count(s => s.CampaignId == campaign.Id);
            var completed = vaccinationRecords.Count(r => r.CampaignId == campaign.Id);
            var data = new VaccinationCampaignDto
            {
                Id = campaign.Id,
                CampaignName = campaign.CampaignName,
                VaccineType = campaign.VaccineType,
                ScheduledDate = campaign.ScheduledDate,
                TargetGrades = campaign.TargetGrades,
                Status = campaign.Status,
                ConsentReceived = trueConsents,
                TotalStudents = totalStudents,
                VaccinationsCompleted = completed

            };

            return ApiResponse<VaccinationCampaignDto>.SuccessResult(data);
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> CreateCampaignAsync(CreateVaccinationCampaignRequest request)
        {
            var entity = _mapper.Map<VaccinationCampaign>(request);
            entity.Status = VaccinationStatus.Planned;
            entity.ScheduledDate = DateTime.UtcNow;
            await _unitOfWork.VaccinationCampaignRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<VaccinationCampaignDto>(entity);
            return ApiResponse<VaccinationCampaignDto>.SuccessResult(dto, "Tạo chiến dịch tiêm chủng thành công.");
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> UpdateCampaignAsync(int recordId, UpdateVaccinationCampaignRequest request)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(recordId);
            if (campaign == null)
                return ApiResponse<VaccinationCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");

            _mapper.Map(request, campaign);
            _unitOfWork.VaccinationCampaignRepository.Update(campaign);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<VaccinationCampaignDto>(campaign);
            return ApiResponse<VaccinationCampaignDto>.SuccessResult(dto, "Cập nhật thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteCampaignAsync(int id)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy chiến dịch.");

            _unitOfWork.VaccinationCampaignRepository.Delete(campaign);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResult(true, "Xóa chiến dịch thành công.");
        }

        public async Task<ApiResponse<List<StudentDto>>> GetVaccinatedStudentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null)
                return ApiResponse<List<StudentDto>>.ErrorResult("Không tìm thấy lịch tiêm chủng hoặc lịch tiêm chủng không tồn tại");

            var vaccinationRecords = await _unitOfWork.VaccinationRecordRepository
                .Query().Where(r => r.CampaignId == campaignId).ToListAsync();

            if (vaccinationRecords == null || !vaccinationRecords.Any())
                return ApiResponse<List<StudentDto>>.ErrorResult("Hiện tại không có học sinh nào trong chiến dịch này.");

            var studentIds = vaccinationRecords.Select(r => r.StudentId).Distinct().ToList();
            var students = await _unitOfWork.StudentRepository
                .Query().Include(s => s.Parent).Include(s => s.MedicalProfile)
                .Where(s => studentIds.Contains(s.Id)).ToListAsync();

            if (students == null || students.Count == 0)
                return ApiResponse<List<StudentDto>>.ErrorResult("Không tìm thấy học sinh tương ứng.");

            var dtos = _mapper.Map<List<StudentDto>>(students);
            return ApiResponse<List<StudentDto>>.SuccessResult(dtos);
        }

    }
}