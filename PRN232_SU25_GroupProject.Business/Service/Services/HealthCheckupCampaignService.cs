using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class HealthCheckupCampaignService : IHealthCheckupCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HealthCheckupCampaignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<HealthCheckupCampaignDto>>> GetAllCampaignsAsync()
        {
            var campaigns = await _unitOfWork.HealthCheckupCampaignRepository.GetAllAsync();
            var results = await _unitOfWork.HealthCheckupResultRepository.GetAllAsync();
            var healthconsents = await _unitOfWork.MedicalConsentRepository.Query()
                .Where(s => s.ConsentType == ConsentType.HealthCheckup)
                .ToListAsync();

            var trueconsents = healthconsents.Where(s => s.ConsentGiven == true);
            var dtos = campaigns.Select(c =>
            {

                var campaignResults = results.Where(r => r.CampaignId == c.Id).ToList();
                var completed = campaignResults.Count;
                var needFollowup = campaignResults.Count(r => r.RequiresFollowup);

                return new HealthCheckupCampaignDto
                {
                    Id = c.Id,
                    CampaignName = c.CampaignName,
                    CheckupTypes = c.CheckupTypes,
                    ScheduledDate = c.ScheduledDate,
                    TargetGrades = c.TargetGrades,
                    Status = c.Status,
                    ConsentReceived = trueconsents.Count(s => s.CampaignId == c.Id),
                    TotalStudents = healthconsents.Count(s => s.CampaignId == c.Id),
                    CheckupsCompleted = completed,
                    RequiringFollowup = needFollowup
                };
            }).ToList();
            return ApiResponse<List<HealthCheckupCampaignDto>>.SuccessResult(dtos, "Lấy danh sách chiến dịch thành công.");
        }

        public async Task<ApiResponse<HealthCheckupCampaignDto>> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<HealthCheckupCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");
            var results = await _unitOfWork.HealthCheckupResultRepository.GetAllAsync();
            var healthconsents = await _unitOfWork.MedicalConsentRepository.Query()
                .Where(s => s.ConsentType == ConsentType.HealthCheckup)
                .ToListAsync();
            var trueconsents = healthconsents.Where(s => s.ConsentGiven == true);
            var totalstudent = healthconsents.Count(s => s.CampaignId == campaign.Id);
            var completed = results.Count(r => r.CampaignId == campaign.Id);
            var data = new HealthCheckupCampaignDto
            {
                Id = campaign.Id,
                CampaignName = campaign.CampaignName,
                CheckupTypes = campaign.CheckupTypes,
                ScheduledDate = campaign.ScheduledDate,
                TargetGrades = campaign.TargetGrades,
                Status = campaign.Status,
                ConsentReceived = trueconsents.Count(s => s.CampaignId == campaign.Id),
                TotalStudents = totalstudent,
                CheckupsCompleted = completed,
                RequiringFollowup = results.Count(r => r.CampaignId == campaign.Id && r.RequiresFollowup),
            };
            return ApiResponse<HealthCheckupCampaignDto>.SuccessResult(data);
        }

        public async Task<ApiResponse<HealthCheckupCampaignDto>> CreateCampaignAsync(CreateCheckupCampaignRequest request)
        {
            var campaign = _mapper.Map<HealthCheckupCampaign>(request);
            campaign.Status = CheckupStatus.Planned;
            campaign.ScheduledDate = DateTime.UtcNow;
            await _unitOfWork.HealthCheckupCampaignRepository.AddAsync(campaign);
            await _unitOfWork.SaveChangesAsync();
            var data = _mapper.Map<HealthCheckupCampaignDto>(campaign);
            return ApiResponse<HealthCheckupCampaignDto>.SuccessResult(data, "Tạo chiến dịch thành công.");
        }

        public async Task<ApiResponse<HealthCheckupCampaignDto>> UpdateCampaignAsync(UpdateCheckupCampaignRequest request)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(request.Id);
            if (campaign == null)
                return ApiResponse<HealthCheckupCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");
            _mapper.Map(request, campaign);
            _unitOfWork.HealthCheckupCampaignRepository.Update(campaign);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<HealthCheckupCampaignDto>.SuccessResult(_mapper.Map<HealthCheckupCampaignDto>(campaign), "Cập nhật thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteCampaignAsync(int id)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy chiến dịch.");
            _unitOfWork.HealthCheckupCampaignRepository.Delete(campaign);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa chiến dịch thành công.");
        }

        public async Task<ApiResponse<List<StudentDto>>> GetScheduledStudentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null)
                return ApiResponse<List<StudentDto>>.ErrorResult("Không tìm thấy lịch khám hoặc lịch khám không tồn tại");
            var campaignResults = await _unitOfWork.HealthCheckupResultRepository
                .Query().Where(r => r.CampaignId == campaignId).ToListAsync();

            if (campaignResults == null || !campaignResults.Any())
                return ApiResponse<List<StudentDto>>.ErrorResult("Hiện tại không có học sinh nào trong chiến dịch này.");

            var studentIds = campaignResults.Select(r => r.StudentId).Distinct().ToList();
            var students = await _unitOfWork.StudentRepository
                .Query().Include(s => s.Parent).Include(s => s.MedicalProfile)
                .Where(s => studentIds.Contains(s.Id)).ToListAsync();

            if (students == null || students.Count == 0)
                return ApiResponse<List<StudentDto>>.ErrorResult("Không tìm thấy học sinh tương ứng.");

            var data = _mapper.Map<List<StudentDto>>(students);
            return ApiResponse<List<StudentDto>>.SuccessResult(data);
        }
    }

}
