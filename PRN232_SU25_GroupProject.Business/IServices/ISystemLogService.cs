using Microsoft.AspNetCore.OData.Results;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface ISystemLogService
    {
        Task LogActionAsync(LogActionRequest request);
        Task<PagedResult<SystemLog>> GetSystemLogsAsync(SystemLogFilterRequest filter);
        Task<SystemLog> GetSystemLogByIdAsync(int logId);
        Task<SystemLogStatistics> GetLogStatisticsAsync(DateTime? from = null, DateTime? to = null);
        Task CleanupOldLogsAsync(int daysToKeep = 90);
    }
}
