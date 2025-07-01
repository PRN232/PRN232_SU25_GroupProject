using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResult> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !user.IsActive)
                return new LoginResult { Success = false, ErrorMessage = "Tài khoản không tồn tại hoặc đã bị vô hiệu hóa." };

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded)
                return new LoginResult { Success = false, ErrorMessage = "Sai mật khẩu." };

            // Tạo token
            var token = GenerateJwtToken(user, out DateTime expiresAt);
            Console.WriteLine($"tạo token thành công : {token}");
            // Lấy thông tin chi tiết người dùng (tùy theo role)
            var userDto = await BuildUserDtoAsync(user);

            return new LoginResult
            {
                Success = true,
                Token = token,
                User = userDto,
                ExpiresAt = expiresAt
            };
        }

        public async Task<bool> LogoutAsync(int userId)
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<User> GetCurrentUserAsync()
        {
            Console.WriteLine($"_httpContextAccessor.HttpContext.User =" + _httpContextAccessor.HttpContext.User);
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            Console.WriteLine($"Current User ID: {userId}");
            if (int.TryParse(userId, out int id))
                return await _userManager.FindByIdAsync(userId);
            return null;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return false;

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            return result.Succeeded;
        }

        private string GenerateJwtToken(User user, out DateTime expiresAt)
        {
            var jwtSection = _configuration.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            expiresAt = DateTime.Now.AddHours(20);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(ClaimTypes.Role, user.Role.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id.ToString())
    };

            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            Console.WriteLine($"issuer: {issuer}");
            Console.WriteLine($"audience: {audience}");
            Console.WriteLine($"keyD: {jwtSection["SecretKey"]}");

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private async Task<UserDto> BuildUserDtoAsync(User user)
        {
            var dto = _mapper.Map<UserDto>(user);

            // Gán FullName từ entity cụ thể tùy theo Role
            if (user.Role == UserRole.Parent)
            {
                var parent = await _unitOfWork.GetRepository<Parent>()
                    .Query().FirstOrDefaultAsync(p => p.UserId == user.Id);
                dto.PhoneNumber = parent?.PhoneNumber;
            }

            // Add nurse/admin/etc. logic here if needed

            return dto;
        }
    }
}
