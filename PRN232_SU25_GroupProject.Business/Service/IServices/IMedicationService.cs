using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IMedicationService
    {
        Task<List<MedicationDto>> GetAllMedicationsAsync();
        Task<MedicationDto> GetMedicationByIdAsync(int id);
        Task<bool> UpdateStockAsync(int medicationId, int quantity);
        Task<List<MedicationDto>> GetExpiringMedicationsAsync(DateTime beforeDate);
    }


}
