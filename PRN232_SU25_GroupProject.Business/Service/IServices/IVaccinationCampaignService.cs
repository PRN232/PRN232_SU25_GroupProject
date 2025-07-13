using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IVaccinationCampaignService
    {
        Task<ApiResponse<List<VaccinationCampaignDto>>> GetAllCampaignsAsync();
        Task<ApiResponse<VaccinationCampaignDto>> GetCampaignByIdAsync(int id);
        Task<ApiResponse<VaccinationCampaignDto>> CreateCampaignAsync(CreateVaccinationCampaignRequest request);
        Task<ApiResponse<VaccinationCampaignDto>> UpdateCampaignAsync(int campaignId, UpdateVaccinationCampaignRequest request);
        Task<ApiResponse<bool>> DeleteCampaignAsync(int id);
        // Lấy danh sách học sinh đã tham gia chiến dịch tiêm chủng (nếu cần)
        Task<ApiResponse<List<StudentDto>>> GetVaccinatedStudentsAsync(int campaignId);
    }

}
