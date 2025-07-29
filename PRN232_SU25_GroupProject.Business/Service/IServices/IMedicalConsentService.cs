using PRN232_SU25_GroupProject.Business.DTOs.MedicalConsents;
using PRN232_SU25_GroupProject.DataAccess.Enums;
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
        /// <summary>
        /// Lấy danh sách các học sinh trong tất cả các lớp thuộc TargetGrades của chiến dịch,
        /// kèm trạng thái consent (có/không có, đã đồng ý hay chưa).
        /// </summary>
        Task<ApiResponse<List<StudentConsentStatusDto>>>
            GetConsentStatusByCampaignAsync(int campaignId, ConsentType consentType);

        /// <summary>
        /// Tương tự nhưng chỉ filter 1 class cụ thể.
        /// </summary>
        Task<ApiResponse<List<StudentConsentStatusDto>>>
            GetConsentStatusByCampaignAndClassAsync(int campaignId, ConsentType consentType, string className);
    }


}
