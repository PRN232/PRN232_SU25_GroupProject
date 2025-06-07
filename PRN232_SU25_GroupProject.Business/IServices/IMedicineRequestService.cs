using Microsoft.AspNetCore.OData.Results;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IMedicineRequestService
    {
        Task<PagedResult<MedicineRequest>> GetMedicineRequestsAsync(MedicineRequestFilterRequest filter);
        Task<MedicineRequest> GetMedicineRequestByIdAsync(int requestId);
        Task<MedicineRequest> CreateMedicineRequestAsync(CreateMedicineRequestRequest request);
        Task<MedicineRequest> UpdateMedicineRequestAsync(int requestId, UpdateMedicineRequestRequest request);
        Task<bool> ApproveMedicineRequestAsync(int requestId, int approvedBy);
        Task<bool> RejectMedicineRequestAsync(int requestId, int rejectedBy, string reason);
        Task<IEnumerable<MedicineRequest>> GetPendingRequestsAsync(int schoolId);
        Task<IEnumerable<MedicineRequest>> GetRequestsByParentAsync(int parentId);
        Task<IEnumerable<MedicineRequest>> GetRequestsByStudentAsync(int studentId);
    }

}
