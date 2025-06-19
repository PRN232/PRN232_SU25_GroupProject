using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
