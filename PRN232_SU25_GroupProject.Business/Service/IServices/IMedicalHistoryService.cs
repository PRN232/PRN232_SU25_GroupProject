using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalHistoryService
    {
        Task<ApiResponse<List<MedicalHistoryDto>>> GetMedicalHistoriesByProfileAsync(int medicalProfileId);
        Task<ApiResponse<MedicalHistoryDto>> GetMedicalHistoryByIdAsync(int medicalHistoryId);
        Task<ApiResponse<MedicalHistoryDto>> CreateMedicalHistoryAsync(CreateMedicalHistoryRequest request);
        Task<ApiResponse<MedicalHistoryDto>> UpdateMedicalHistoryAsync(UpdateMedicalHistoryRequest request);
        Task<ApiResponse<bool>> DeleteMedicalHistoryAsync(int medicalHistoryId);
    }
}
