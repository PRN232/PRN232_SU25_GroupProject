using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IAuditService
    {
        Task LogAuditAsync(AuditLogRequest request);
        Task<PagedResult<AuditLog>> GetAuditLogsAsync(AuditLogFilterRequest filter);
        Task<AuditTrail> GetEntityAuditTrailAsync(string entityType, int entityId);
    }
}
