using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;

namespace PRN232_SU25_GroupProject.Presentation.Controllers
{
    /// <summary>
    /// Xử lý xác thực & tài khoản người dùng
    /// </summary>
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// Đăng nhập người dùng và trả về token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/auth/login
        ///     Chọn 1 trong các tài khoản sau để đăng nhập:
        ///     *****Tài khoản role Admin*****
        ///     {
        ///        "email": "admin@school.vn",
        ///        "password": "String_1",
        ///        "rememberMe": true
        ///     }
        ///     *****Tài khoản role Manager*****
        ///     {
        ///        "email": "manager@school.vn",
        ///        "password": "String_1",
        ///        "rememberMe": true
        ///     }
        ///     *****Tài khoản role Nurse*****
        ///     {
        ///        "email": "nurse1@school.vn",
        ///        "password": "String_1",
        ///        "rememberMe": true
        ///     }
        ///     *****Tài khoản role Parent*****
        ///     {
        ///        "email": "p1@parent.vn",
        ///        "password": "String_1",
        ///        "rememberMe": true
        ///     }
        /// </remarks>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);
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
