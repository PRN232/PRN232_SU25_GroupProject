using Microsoft.AspNetCore.Identity.Data;
using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IAuthService
    {
        Task<LoginResult> LoginAsync(PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication.LoginRequest request);
        Task<bool> LogoutAsync(int userId);
        Task<User> GetCurrentUserAsync();
        Task<bool> ChangePasswordAsync(ChangePasswordRequest request);
    }
}
