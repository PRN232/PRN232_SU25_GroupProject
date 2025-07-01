using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
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

        // 1. Lấy tất cả chiến dịch
        public async Task<ApiResponse<List<HealthCheckupCampaignDto>>> GetAllCampaignsAsync()
        {
            var repo = _unitOfWork.HealthCheckupCampaignRepository;
            var campaigns = await repo.GetAllAsync();
            var data = _mapper.Map<List<HealthCheckupCampaignDto>>(campaigns);
            return ApiResponse<List<HealthCheckupCampaignDto>>.SuccessResult(data, "Lấy danh sách chiến dịch thành công.");
        }

        // 2. Tạo chiến dịch mới
        public async Task<ApiResponse<HealthCheckupCampaignDto>> CreateCampaignAsync(CreateCheckupCampaignRequest request)
        {
            var campaign = _mapper.Map<HealthCheckupCampaign>(request);
            campaign.Status = DataAccess.Enums.CheckupStatus.Planned;
            campaign.ScheduledDate = DateTime.UtcNow;

            await _unitOfWork.HealthCheckupCampaignRepository.AddAsync(campaign);
            await _unitOfWork.SaveChangesAsync();

            var data = _mapper.Map<HealthCheckupCampaignDto>(campaign);
            return ApiResponse<HealthCheckupCampaignDto>.SuccessResult(data, "Tạo chiến dịch thành công.");
        }

        // 3. Lấy thông tin chiến dịch theo ID
        public async Task<ApiResponse<HealthCheckupCampaignDto>> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<HealthCheckupCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");
            var data = _mapper.Map<HealthCheckupCampaignDto>(campaign);
            return ApiResponse<HealthCheckupCampaignDto>.SuccessResult(data);
        }

        // 4. Lấy học sinh theo chiến dịch
        public async Task<ApiResponse<List<StudentDto>>> GetScheduledStudentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(campaignId);
            if (campaign == null)
                return ApiResponse<List<StudentDto>>.ErrorResult("Không tìm thấy chiến dịch.");

            var students = await _unitOfWork.StudentRepository
                .Query()
                .Include(s => s.Parent)
        .Include(s => s.MedicalProfile)
                .Where(s => s.Id == campaign.StudentId)
                .ToListAsync();

            var data = _mapper.Map<List<StudentDto>>(students);
            return ApiResponse<List<StudentDto>>.SuccessResult(data);
        }

        // 5. Gửi thông báo cho phụ huynh
        public async Task<ApiResponse<string>> SendNotificationToParentsAsync(int campaignId)
        {
            var campaign = await _unitOfWork.HealthCheckupCampaignRepository.GetByIdAsync(campaignId);
            if (campaign == null)
                return ApiResponse<string>.ErrorResult("Không tìm thấy chiến dịch.");

            // TODO: Thực hiện gửi thông báo thật sự tại đây.
            return ApiResponse<string>.SuccessResult(null, "Gửi thông báo thành công!");
        }

        // 6. Ghi nhận kết quả khám
        public async Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultAsync(RecordCheckupRequest request)
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

            return ApiResponse<HealthCheckupResultDto>.SuccessResult(dto, "Ghi nhận kết quả thành công!");
        }

        // 7. Lịch sử khám của học sinh
        public async Task<ApiResponse<List<HealthCheckupResultDto>>> GetStudentCheckupHistoryAsync(int studentId, int? currentUserId, string currentUserRole)
        {
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
                return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Không tìm thấy học sinh này.");

            if (currentUserRole == "Parent")
            {
                if (currentUserId == null)
                    return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Bạn không có quyền truy cập thông tin này.");

                var parent = await _unitOfWork.ParentRepository
                    .Query()
                    .FirstOrDefaultAsync(p => p.UserId == currentUserId.Value);

                if (parent == null)
                    return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Không xác định được phụ huynh với tài khoản này.");

                if (student.ParentId != parent.Id)
                    return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Bạn không có quyền truy cập thông tin học sinh này.");
            }

            var results = await _unitOfWork.HealthCheckupResultRepository
                .Query().Where(r => r.StudentId == studentId).ToListAsync();

            if (results == null || !results.Any())
                return ApiResponse<List<HealthCheckupResultDto>>.ErrorResult("Học sinh chưa có lịch sử kiểm tra sức khỏe.");

            var studentDto = _mapper.Map<StudentDto>(student);
            var resultDtos = results.Select(r =>
            {
                var dto = _mapper.Map<HealthCheckupResultDto>(r);
                dto.StudentName = studentDto.FullName;
                dto.StudentCode = studentDto.StudentCode;
                return dto;
            }).ToList();

            return ApiResponse<List<HealthCheckupResultDto>>.SuccessResult(resultDtos);
        }

        // 8. Gửi kết quả cho phụ huynh
        public async Task<ApiResponse<string>> SendResultToParentAsync(int resultId)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null)
                return ApiResponse<string>.ErrorResult("Không tìm thấy kết quả kiểm tra.");

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(result.StudentId);
            var parent = student?.Parent;
            if (parent == null)
                return ApiResponse<string>.ErrorResult("Không tìm thấy phụ huynh của học sinh này.");

            // TODO: Thực hiện gửi thông báo thực tế
            return ApiResponse<string>.SuccessResult(null, "Gửi kết quả cho phụ huynh thành công!");
        }

        // 9. Lên lịch tái khám
        public async Task<ApiResponse<string>> ScheduleFollowupAsync(int resultId, DateTime appointmentDate)
        {
            var result = await _unitOfWork.HealthCheckupResultRepository.GetByIdAsync(resultId);
            if (result == null)
                return ApiResponse<string>.ErrorResult("Không tìm thấy kết quả kiểm tra.");

            result.RequiresFollowup = true;
            result.Recommendations = $"Tái khám vào ngày {appointmentDate:dd/MM/yyyy}";
            result.CheckupDate = DateTime.UtcNow;

            _unitOfWork.HealthCheckupResultRepository.Update(result);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<string>.SuccessResult(null, "Đã lên lịch hẹn tái khám.");
        }
    }

}
