using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IVaccinationService
    {
        Task<VaccinationCampaignDto> CreateCampaignAsync(CreateVaccinationCampaignRequest request);
        Task<List<VaccinationCampaignDto>> GetActiveCampaignsAsync();
        Task<bool> SendConsentFormsAsync(int campaignId);
        Task<bool> SubmitConsentAsync(SubmitConsentRequest request);
        Task<List<StudentDto>> GetEligibleStudentsAsync(int campaignId);
        Task<VaccinationRecordDto> RecordVaccinationAsync(RecordVaccinationRequest request);
        Task<List<VaccinationRecordDto>> GetStudentVaccinationHistoryAsync(int studentId);
    }

}
