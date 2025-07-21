using PRN232_SU25_GroupProject.Business.DTOs.MedicationGivens;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicationsGivenService
    {
        Task<ApiResponse<List<MedicationGivenDto>>> GetAllMedicationsGivenAsync();
        Task<ApiResponse<MedicationGivenDto>> GetMedicationsGivenByIdAsync(int id);
        Task<ApiResponse<MedicationGivenDto>> CreateMedicationsGivenAsync(CreateMedicationsGivenRequest request);
        Task<ApiResponse<MedicationGivenDto>> UpdateMedicationsGivenAsync(int id, UpdateMedicationsGivenRequest request);
        Task<ApiResponse<bool>> DeleteMedicationsGivenAsync(int id);
    }
}
