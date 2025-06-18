using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(CreateUserRequest request);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetUsersByRoleAsync(UserRole role);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeactivateUserAsync(int id);
    }
}
