using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicationService
    {
        Task<List<Medication>> GetAllMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(int id);
        Task<bool> UpdateStockAsync(int medicationId, int quantity);
        Task<List<Medication>> GetExpiringMedicationsAsync(DateTime beforeDate);
    }


}
