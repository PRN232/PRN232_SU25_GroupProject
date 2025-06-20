using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IStudentMedicationService
    {
        Task<StudentMedicationDto> SubmitMedicationRequestAsync(SubmitMedicationRequest request);
        Task<List<StudentMedicationDto>> GetPendingApprovalsAsync();
        Task<bool> ApproveMedicationAsync(int requestId, int nurseId);
        Task<List<StudentMedicationDto>> GetStudentMedicationsAsync(int studentId);
        Task<bool> AdministerMedicationAsync(AdministerMedicationRequest request);
    }
}
