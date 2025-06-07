using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IVaccinationService
    {
        Task<PagedResult<Vaccination>> GetVaccinationsAsync(VaccinationFilterRequest filter);
        Task<Vaccination> GetVaccinationByIdAsync(int vaccinationId);
        Task<Vaccination> CreateVaccinationAsync(CreateVaccinationRequest request);
        Task<Vaccination> UpdateVaccinationAsync(int vaccinationId, UpdateVaccinationRequest request);
        Task<bool> DeleteVaccinationAsync(int vaccinationId);
        Task<IEnumerable<Vaccination>> GetVaccinationsByStudentAsync(int studentId);
        Task<IEnumerable<Vaccination>> GetVaccinationsByCampaignAsync(int campaignId);
        Task<VaccinationHistory> GetStudentVaccinationHistoryAsync(int studentId);
    }
}
