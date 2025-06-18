using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IVaccinationService
    {
        Task<VaccinationCampaign> CreateCampaignAsync(CreateVaccinationCampaignRequest request);
        Task<List<VaccinationCampaign>> GetActiveCampaignsAsync();
        Task<bool> SendConsentFormsAsync(int campaignId);
        Task<bool> SubmitConsentAsync(SubmitConsentRequest request);
        Task<List<Student>> GetEligibleStudentsAsync(int campaignId);
        Task<VaccinationRecord> RecordVaccinationAsync(RecordVaccinationRequest request);
        Task<List<VaccinationRecord>> GetStudentVaccinationHistoryAsync(int studentId);
    }
}
