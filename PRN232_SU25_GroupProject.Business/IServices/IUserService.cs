using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync(int? schoolId = null);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(CreateUserRequest request);
        Task<User> UpdateUserAsync(int userId, UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> ActivateUserAsync(int userId);
        Task<bool> DeactivateUserAsync(int userId);
        Task<IEnumerable<User>> GetUsersByTypeAsync(UserType userType, int? schoolId = null);
        Task<IEnumerable<User>> GetParentsByStudentIdAsync(int studentId);
    }
}
