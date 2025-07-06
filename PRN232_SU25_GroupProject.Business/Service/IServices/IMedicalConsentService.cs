using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalConsents;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalConsentService
    {
        Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByStudentAsync(int studentId);
        Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByCampaignAsync(int campaignId);
        Task<ApiResponse<MedicalConsentDto>> GetConsentByIdAsync(int id);

        Task<ApiResponse<MedicalConsentDto>> CreateMedicalConsentAsync(CreateMedicalConsentRequest request);
        Task<ApiResponse<List<MedicalConsentDto>>> CreateMedicalConsentForClassAsync(CreateMedicalConsentClassRequest request);

        Task<ApiResponse<MedicalConsentDto>> UpdateMedicalConsentAsync(int id, UpdateMedicalConsentRequest request);
        Task<ApiResponse<bool>> DeleteMedicalConsentAsync(int id);
    }


}
