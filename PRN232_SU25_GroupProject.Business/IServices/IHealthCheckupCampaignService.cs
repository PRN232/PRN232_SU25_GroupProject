using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IHealthCheckupCampaignService
    {
        Task<PagedResult<HealthCheckupCampaign>> GetHealthCheckupCampaignsAsync(CampaignFilterRequest filter);
        Task<HealthCheckupCampaign> GetHealthCheckupCampaignByIdAsync(int campaignId);
        Task<HealthCheckupCampaign> CreateHealthCheckupCampaignAsync(CreateHealthCheckupCampaignRequest request);
        Task<HealthCheckupCampaign> UpdateHealthCheckupCampaignAsync(int campaignId, UpdateHealthCheckupCampaignRequest request);
        Task<bool> DeleteHealthCheckupCampaignAsync(int campaignId);
        Task<bool> StartCampaignAsync(int campaignId);
        Task<bool> CompleteCampaignAsync(int campaignId);
        Task<HealthCheckupCampaignStatistics> GetCampaignStatisticsAsync(int campaignId);
    }
}
