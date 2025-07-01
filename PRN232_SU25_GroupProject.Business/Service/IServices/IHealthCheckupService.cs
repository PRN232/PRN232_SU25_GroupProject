using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupService
    {
        Task<HealthCheckupCampaignDto> CreateCampaignAsync(CreateCheckupCampaignRequest request);
        Task<List<HealthCheckupCampaignDto>> GetAllCampaignsAsync();
        Task<HealthCheckupCampaignDto> GetCampaignByIdAsync(int id);
        Task<bool> SendNotificationToParentsAsync(int campaignId);
        Task<List<StudentDto>> GetScheduledStudentsAsync(int campaignId);
        Task<HealthCheckupResultDto> RecordCheckupResultAsync(RecordCheckupRequest request);
        Task<List<HealthCheckupResultDto>> GetStudentCheckupHistoryAsync(int studentId);
        Task<bool> SendResultToParentAsync(int resultId);
        Task<bool> ScheduleFollowupAsync(int resultId, DateTime appointmentDate);
        Task<StudentDto> GetStudentByIdAsync(int studentId);

    }

}
