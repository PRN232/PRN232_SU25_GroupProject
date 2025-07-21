using PRN232_SU25_GroupProject.Business.DTOs.MedicalConsents;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalConsentService
    {
        Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByStudentAsync(int studentId);
        Task<ApiResponse<List<MedicalConsentDto>>> GetConsentsByCampaignAsync(int campaignId);
        Task<ApiResponse<MedicalConsentDto>> GetConsentByIdAsync(int id);

        Task<ApiResponse<MedicalConsentDto>> CreateMedicalConsentAsync(CreateMedicalConsentRequest request);
        Task<ApiResponse<List<MedicalConsentDto>>> CreateMedicalConsentForClassAsync(CreateMedicalConsentClassRequest request);

        Task<ApiResponse<MedicalConsentDto>> UpdateMedicalConsentAsync(int id, UpdateMedicalConsentRequest request, int currentUserId);
        Task<ApiResponse<bool>> DeleteMedicalConsentAsync(int id);
    }


}
