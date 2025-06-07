using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicineService
    {
        Task<PagedResult<Medicine>> GetMedicinesAsync(MedicineFilterRequest filter);
        Task<Medicine> GetMedicineByIdAsync(int medicineId);
        Task<Medicine> GetMedicineByCodeAsync(string medicineCode);
        Task<Medicine> CreateMedicineAsync(CreateMedicineRequest request);
        Task<Medicine> UpdateMedicineAsync(int medicineId, UpdateMedicineRequest request);
        Task<bool> DeleteMedicineAsync(int medicineId);
        Task<IEnumerable<Medicine>> GetLowStockMedicinesAsync(int schoolId);
        Task<IEnumerable<Medicine>> GetExpiredMedicinesAsync(int schoolId);
        Task<bool> UpdateMedicineQuantityAsync(int medicineId, int quantity, string reason);
        Task<MedicineInventoryReport> GetInventoryReportAsync(int schoolId);
    }

