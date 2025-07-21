using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

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
