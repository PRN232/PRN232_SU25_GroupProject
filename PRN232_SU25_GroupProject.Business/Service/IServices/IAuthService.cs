using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IAuthService
    {
        Task<ApiResponse<LoginResult>> LoginAsync(PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication.LoginRequest request);
        Task<ApiResponse<bool>> LogoutAsync(int userId);
        Task<ApiResponse<UserDto>> GetCurrentUserAsync();
        Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request);
        Task<ApiResponse<LoginResult>> RegisterParentAsync(RegisterParentRequest request);
    }
}
