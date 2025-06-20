using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IUserService
    {
        Task<UserDto> CreateUserAsync(CreateUserRequest request);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<List<UserDto>> GetUsersByRoleAsync(UserRole role);
        Task<bool> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeactivateUserAsync(int id);
    }

}
