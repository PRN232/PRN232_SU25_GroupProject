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
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        #region Đăng nhập
        /// <summary>
        /// Đăng nhập, trả về JWT & thông tin user
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(request);

            return result.Success
                ? Ok(result)                     // 200 + body = token + user
                : Unauthorized(new { result.ErrorMessage });
        }
        #endregion

        #region Đăng xuất
        /// <summary>
        /// Đăng xuất (client chỉ cần xoá token, server chỉ ghi log SignOut)
        /// </summary>
        [HttpPost("logout")]
        [Authorize]                                        // cần token hợp lệ
        public async Task<IActionResult> Logout()
        {
            var user = await _authService.GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            await _authService.LogoutAsync(user.Id);
            return NoContent();                            // 204
        }
        #endregion

        #region Lấy hồ sơ hiện tại
        /// <summary>
        /// Trả về thông tin người dùng hiện tại dựa trên JWT
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {

            foreach (var c in User.Claims)
                Console.WriteLine($"{c.Type}: {c.Value}");

            var user = await _authService.GetCurrentUserAsync();
            if (user == null)
                return Unauthorized();

            // Sử dụng lại hàm BuildUserDtoAsync qua LoginAsync nếu bạn expose public,
            // còn không hãy map tại đây (tuỳ cách bạn thiết kế service).
            var profileDto = new
            {
                user.Id,
                user.Email,
                user.Role,
                user.IsActive
            };

            return Ok(profileDto);
        }
        #endregion

        #region Đổi mật khẩu
        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            // Ép userId trong payload = userId của token để tránh đổi mật khẩu hộ người khác
            var currentUser = await _authService.GetCurrentUserAsync();
            if (currentUser == null) return Unauthorized();

            request.UserId = currentUser.Id;

            var succeeded = await _authService.ChangePasswordAsync(request);
            return succeeded ? NoContent() : BadRequest("Đổi mật khẩu thất bại.");
        }
        #endregion
    }
}
