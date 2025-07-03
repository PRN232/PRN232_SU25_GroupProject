using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.ChronicDisease;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IChronicDiseaseService
    {
        Task<ApiResponse<List<ChronicDiseaseDto>>> GetChronicDiseasesByProfileAsync(int medicalProfileId);
        Task<ApiResponse<ChronicDiseaseDto>> GetChronicDiseaseByIdAsync(int chronicDiseaseId);
        Task<ApiResponse<ChronicDiseaseDto>> CreateChronicDiseaseAsync(CreateChronicDiseaseRequest request);
        Task<ApiResponse<ChronicDiseaseDto>> UpdateChronicDiseaseAsync(UpdateChronicDiseaseRequest request);
        Task<ApiResponse<bool>> DeleteChronicDiseaseAsync(int chronicDiseaseId);
    }
}
