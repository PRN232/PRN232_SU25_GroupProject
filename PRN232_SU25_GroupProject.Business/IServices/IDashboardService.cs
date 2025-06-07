using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IDashboardService
    {
        Task<DashboardData> GetSchoolDashboardAsync(int schoolId);
        Task<DashboardData> GetParentDashboardAsync(int parentId);
        Task<DashboardData> GetHealthStaffDashboardAsync(int userId, int schoolId);
        Task<DashboardData> GetAdminDashboardAsync();
    }

}
