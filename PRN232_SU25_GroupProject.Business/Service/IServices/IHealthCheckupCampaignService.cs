using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupCampaignService
    {
        Task<ApiResponse<List<HealthCheckupCampaignDto>>> GetAllCampaignsAsync();
        Task<ApiResponse<HealthCheckupCampaignDto>> GetCampaignByIdAsync(int id);
        Task<ApiResponse<HealthCheckupCampaignDto>> CreateCampaignAsync(CreateCheckupCampaignRequest request);
        Task<ApiResponse<HealthCheckupCampaignDto>> UpdateCampaignAsync(UpdateCheckupCampaignRequest request);
        Task<ApiResponse<bool>> DeleteCampaignAsync(int id);
        Task<ApiResponse<List<StudentDto>>> GetScheduledStudentsAsync(int campaignId);
    }


}
