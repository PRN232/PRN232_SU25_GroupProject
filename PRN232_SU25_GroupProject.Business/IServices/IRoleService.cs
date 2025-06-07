using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IRoleService
    {
        Task<bool> HasPermissionAsync(int userId, string permission);
        Task<IEnumerable<string>> GetUserPermissionsAsync(int userId);
        Task<bool> AssignRoleToUserAsync(int userId, UserType role);
    }

}
