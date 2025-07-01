using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupService
    {
        // 1. Lấy tất cả các chiến dịch
        Task<ApiResponse<List<HealthCheckupCampaignDto>>> GetAllCampaignsAsync();

        // 2. Tạo chiến dịch mới
        Task<ApiResponse<HealthCheckupCampaignDto>> CreateCampaignAsync(CreateCheckupCampaignRequest request);

        // 3. Lấy thông tin chiến dịch theo ID
        Task<ApiResponse<HealthCheckupCampaignDto>> GetCampaignByIdAsync(int id);

        // 4. Lấy danh sách học sinh được kiểm tra trong chiến dịch
        Task<ApiResponse<List<StudentDto>>> GetScheduledStudentsAsync(int campaignId);

        // 5. Gửi thông báo đến phụ huynh
        Task<ApiResponse<string>> SendNotificationToParentsAsync(int campaignId);

        // 6. Ghi nhận kết quả kiểm tra sức khỏe
        Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultAsync(RecordCheckupRequest request);

        // 7. Xem lịch sử kiểm tra sức khỏe của học sinh
        Task<ApiResponse<List<HealthCheckupResultDto>>> GetStudentCheckupHistoryAsync(int studentId, int? currentUserId, string currentUserRole);

        // 8. Gửi kết quả kiểm tra sức khỏe cho phụ huynh
        Task<ApiResponse<string>> SendResultToParentAsync(int resultId);

        // 9. Lên lịch hẹn tái khám cho học sinh
        Task<ApiResponse<string>> ScheduleFollowupAsync(int resultId, DateTime appointmentDate);


    }

}
