using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Xử lý xác thực & tài khoản người dùng
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userIdStr = User.Claims.FirstOrDefault(x => x.Type == "sub" || x.Type == "uid" || x.Type == "UserId")?.Value;
            if (!int.TryParse(userIdStr, out int userId))
                return Unauthorized(ApiResponse<bool>.ErrorResult("Không xác thực được người dùng."));
            var response = await _authService.LogoutAsync(userId);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var response = await _authService.GetCurrentUserAsync();
            if (!response.Success) return Unauthorized(response);
            return Ok(response);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var response = await _authService.ChangePasswordAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("register-parent")]
        public async Task<IActionResult> RegisterParent([FromBody] RegisterParentRequest request)
        {
            var response = await _authService.RegisterParentAsync(request);
            if (!response.Success) return BadRequest(response);
            return Ok(response);
        }
    }
}
