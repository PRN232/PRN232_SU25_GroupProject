using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IVaccinationCampaignService
    {
        Task<PagedResult<VaccinationCampaign>> GetVaccinationCampaignsAsync(CampaignFilterRequest filter);
        Task<VaccinationCampaign> GetVaccinationCampaignByIdAsync(int campaignId);
        Task<VaccinationCampaign> CreateVaccinationCampaignAsync(CreateVaccinationCampaignRequest request);
        Task<VaccinationCampaign> UpdateVaccinationCampaignAsync(int campaignId, UpdateVaccinationCampaignRequest request);
        Task<bool> DeleteVaccinationCampaignAsync(int campaignId);
        Task<bool> StartCampaignAsync(int campaignId);
        Task<bool> CompleteCampaignAsync(int campaignId);
        Task<VaccinationCampaignStatistics> GetCampaignStatisticsAsync(int campaignId);
    }
}
