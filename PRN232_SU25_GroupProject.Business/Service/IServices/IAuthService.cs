using PRN232_SU25_GroupProject.Business.DTOs.Authentication;
using PRN232_SU25_GroupProject.Business.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest request);
        Task<ApiResponse<UserDto>> GetCurrentUserAsync();
        Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request);
        Task<ApiResponse<LoginResult>> RegisterParentAsync(RegisterParentRequest request);
    }
}
