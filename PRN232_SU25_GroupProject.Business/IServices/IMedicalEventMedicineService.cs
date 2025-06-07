using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicalEventMedicineService
    {
        Task<IEnumerable<MedicalEventMedicine>> GetMedicinesByEventAsync(int eventId);
        Task<MedicalEventMedicine> AddMedicineToEventAsync(CreateMedicalEventMedicineRequest request);
        Task<bool> RemoveMedicineFromEventAsync(int eventMedicineId);
        Task<MedicalEventMedicine> UpdateEventMedicineAsync(int eventMedicineId, UpdateMedicalEventMedicineRequest request);
    }

}
