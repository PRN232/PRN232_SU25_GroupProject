using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Medications;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IStudentMedicationService
    {
        Task<StudentMedication> SubmitMedicationRequestAsync(SubmitMedicationRequest request);
        Task<List<StudentMedication>> GetPendingApprovalsAsync();
        Task<bool> ApproveMedicationAsync(int requestId, int nurseId);
        Task<List<StudentMedication>> GetStudentMedicationsAsync(int studentId);
        Task<bool> AdministerMedicationAsync(AdministerMedicationRequest request);
    }
}
