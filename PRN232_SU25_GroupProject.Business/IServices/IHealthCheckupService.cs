using Microsoft.AspNetCore.OData.Results;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IHealthCheckupService
    {
        Task<PagedResult<HealthCheckup>> GetHealthCheckupsAsync(HealthCheckupFilterRequest filter);
        Task<HealthCheckup> GetHealthCheckupByIdAsync(int checkupId);
        Task<HealthCheckup> CreateHealthCheckupAsync(CreateHealthCheckupRequest request);
        Task<HealthCheckup> UpdateHealthCheckupAsync(int checkupId, UpdateHealthCheckupRequest request);
        Task<bool> DeleteHealthCheckupAsync(int checkupId);
        Task<IEnumerable<HealthCheckup>> GetHealthCheckupsByStudentAsync(int studentId);
        Task<IEnumerable<HealthCheckup>> GetHealthCheckupsByCampaignAsync(int campaignId);
        Task<bool> NotifyParentAsync(int checkupId);
        Task<HealthCheckupHistory> GetStudentHealthCheckupHistoryAsync(int studentId);

    }
