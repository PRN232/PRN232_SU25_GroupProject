using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class HealthCheckupService : IHealthCheckupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HealthCheckupService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<HealthCheckupCampaignDto>> GetAllCampaignsAsync()
        {
            var repo = _unitOfWork.HealthCheckupCampaignRepository;
            var campaigns = await repo.GetAllAsync();
            return _mapper.Map<List<HealthCheckupCampaignDto>>(campaigns);
        }

        public async Task<HealthCheckupCampaignDto> CreateCampaignAsync(CreateCheckupCampaignRequest request)
        {
            var campaign = _mapper.Map<HealthCheckupCampaign>(request);
            campaign.Status = DataAccess.Enums.CheckupStatus.Planned;
            campaign.ScheduledDate = DateTime.UtcNow;

            await _unitOfWork.HealthCheckupCampaignRepository.AddAsync(campaign);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<HealthCheckupCampaignDto>(campaign);
        }

        public async Task<HealthCheckupCampaignDto> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                throw new Exception("Checkup campaign not found");

            return _mapper.Map<HealthCheckupCampaignDto>(campaign);
        }
        public async Task<List<StudentDto>> GetScheduledStudentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return new();

            var targetGrades = campaign.TargetGrades
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(g => g.Trim())
                .ToList();

            var students = await _unitOfWork.StudentRepository
                .Query()
                .Where(s => targetGrades.Contains(s.ClassName))
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<bool> SendNotificationToParentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null) return false;

            // Giả lập gửi thông báo tại đây
            return true;
        }

        public async Task<HealthCheckupResultDto> RecordCheckupResultAsync(RecordCheckupRequest request)
        {
            var entity = _mapper.Map<HealthCheckupResult>(request);
            await _unitOfWork.HealthCheckupResultRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Truy xuất dữ liệu phụ cho DTO
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(entity.StudentId);
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(entity.CampaignId);
            var nurse = await _unitOfWork.GetRepository<SchoolNurse>().GetByIdAsync(entity.NurseId);

            var dto = _mapper.Map<HealthCheckupResultDto>(entity);
            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.CampaignName = campaign?.CampaignName;
            dto.NurseName = nurse?.FullName;

            return dto;
        }

        public async Task<List<HealthCheckupResultDto>> GetStudentCheckupHistoryAsync(int studentId)
        {
            var results = await _unitOfWork.HealthCheckupResultRepository
                .Query()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            var campaigns = await _unitOfWork.HealthCheckupCampaignRepository.GetAllAsync();
            var nurses = await _unitOfWork.GetRepository<SchoolNurse>().GetAllAsync();

            var resultDtos = results.Select(r =>
            {
                var dto = _mapper.Map<HealthCheckupResultDto>(r);
                dto.StudentName = student?.FullName;
                dto.StudentCode = student?.StudentCode;
                dto.CampaignName = campaigns.FirstOrDefault(c => c.Id == r.CampaignId)?.CampaignName;
                dto.NurseName = nurses.FirstOrDefault(n => n.Id == r.NurseId)?.FullName;
                return dto;
            }).ToList();

            return resultDtos;
        }

        public async Task<bool> SendResultToParentAsync(int resultId)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null) return false;

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(result.StudentId);
            var parent = student?.Parent;

            if (parent == null) return false;

            // Giả lập gửi thông báo
            return true;
        }

        public async Task<bool> ScheduleFollowupAsync(int resultId, DateTime appointmentDate)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null) return false;

            result.RequiresFollowup = true;
            result.Recommendations = $"Tái khám vào ngày {appointmentDate:dd/MM/yyyy}";
            result.CheckupDate = DateTime.UtcNow;

            _unitOfWork.HealthCheckupResultRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
        public async Task<StudentDto> GetStudentByIdAsync(int studentId)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            return _mapper.Map<StudentDto>(student);
        }
    }
}
