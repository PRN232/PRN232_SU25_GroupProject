using PRN232_SU25_GroupProject.Business.Dtos.HealthCheckups;
using PRN232_SU25_GroupProject.Business.DTOs.HealthCheckups;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IHealthCheckupResultService
    {
        Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultAsync(RecordCheckupRequest request);
        Task<ApiResponse<HealthCheckupResultDto>> RecordCheckupResultParentAsync(RecordCheckupRequestParent request);
        Task<ApiResponse<HealthCheckupResultDto>> GetResultByIdAsync(int resultId);

        Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByCampaignAsync(int campaignId);
        Task<ApiResponse<List<HealthCheckupResultDto>>> GetResultsByStudentAsync(int studentId, int? currentUserId, string currentUserRole);
        Task<ApiResponse<HealthCheckupResultDto>> UpdateResultAsync(UpdateCheckupResultRequest request);
        Task<ApiResponse<bool>> DeleteResultAsync(int resultId);
    }



}
