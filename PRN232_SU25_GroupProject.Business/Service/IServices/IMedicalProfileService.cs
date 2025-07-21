using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicalProfileService
    {
        Task<ApiResponse<MedicalProfileDto>> GetByStudentIdAsync(int studentId);
        Task<ApiResponse<MedicalProfileDto>> UpdateAsync(UpdateMedicalProfileRequest request);
    }
}
