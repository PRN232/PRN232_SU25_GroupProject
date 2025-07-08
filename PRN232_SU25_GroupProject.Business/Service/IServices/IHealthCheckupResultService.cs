using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.HealthCheckups;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupResultService
    {
        Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultAsync(RecordCheckupRequest request);
        Task<ApiResponse<HealthCheckupResultDto>> GetResultByIdAsync(int resultId);

        Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByCampaignAsync(int campaignId);
        Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByStudentAsync(int studentId, int? currentUserId, string currentUserRole);
        Task<ApiResponse<HealthCheckupResultDto>> UpdateResultAsync(UpdateCheckupResultRequest request);
        Task<ApiResponse<bool>> DeleteResultAsync(int resultId);
    }



}
