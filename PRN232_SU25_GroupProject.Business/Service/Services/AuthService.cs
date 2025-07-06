using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Authentication;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repository;
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

        public async Task<ApiResponse<LoginResult>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null || !user.IsActive)
                return ApiResponse<LoginResult>.ErrorResult("Tài khoản không tồn tại hoặc đã bị vô hiệu hóa.");

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return ApiResponse<LoginResult>.ErrorResult("Sai mật khẩu.");

            var token = GenerateJwtToken(user, out DateTime expiresAt);
            var userDto = await BuildUserDtoAsync(user);

            return ApiResponse<LoginResult>.SuccessResult(new LoginResult
            {
                Success = true,
                Token = token,
                ExpiresAt = expiresAt
            }, "Đăng nhập thành công.");
        }


        public async Task<ApiResponse<UserDto>> GetCurrentUserAsync()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            if (int.TryParse(userId, out int id))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var dto = await BuildUserDtoAsync(user);
                    return ApiResponse<UserDto>.SuccessResult(dto);
                }
            }
            return ApiResponse<UserDto>.ErrorResult("Không xác định được người dùng hiện tại.");
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy tài khoản.");

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
                return ApiResponse<bool>.SuccessResult(true, "Đổi mật khẩu thành công.");
            return ApiResponse<bool>.ErrorResult("Đổi mật khẩu thất bại.", result.Errors.Select(e => e.Description).ToList());
        }

        public async Task<ApiResponse<LoginResult>> RegisterParentAsync(RegisterParentRequest request)
        {

            if (!IsValidFullName(request.FullName, out string nameError))
                return ApiResponse<LoginResult>.ErrorResult(nameError);
            // Kiểm tra email đã tồn tại chưa
            var exist = await _userManager.FindByEmailAsync(request.Email);
            if (exist != null)
                return ApiResponse<LoginResult>.ErrorResult("Email đã tồn tại.");
            // Validate số điện thoại
            if (!IsValidVietnamesePhone(request.PhoneNumber))
                return ApiResponse<LoginResult>.ErrorResult("Số điện thoại không hợp lệ. Số điện thoại phải gồm 10 hoặc 11 chữ số, bắt đầu bằng số 0 và chỉ chứa ký tự số.");
            // Kiểm tra số điện thoại đã tồn tại chưa
            var parentRepo = _unitOfWork.GetRepository<Parent>();
            var phoneExist = await parentRepo.Query()
                .AnyAsync(p => p.PhoneNumber == request.PhoneNumber);
            if (phoneExist)
                return ApiResponse<LoginResult>.ErrorResult("Số điện thoại đã được sử dụng cho tài khoản khác.");


            // Tạo user
            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Role = DataAccess.Enums.UserRole.Parent,
                PhoneNumber = request.PhoneNumber,
                IsActive = true,

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return ApiResponse<LoginResult>.ErrorResult("Tạo tài khoản thất bại.", result.Errors.Select(e => e.Description).ToList());

            // Tạo bản ghi Parent
            var parent = new Parent
            {
                UserId = user.Id,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address
            };
            await _unitOfWork.GetRepository<Parent>().AddAsync(parent);
            await _unitOfWork.SaveChangesAsync();

            // Đăng nhập trả luôn token
            var token = "Login để lấy token";
            var userDto = await BuildUserDtoAsync(user);

            var loginResult = new LoginResult
            {
                Success = true,
                Token = token,


            };
            return ApiResponse<LoginResult>.SuccessResult(loginResult, "Đăng ký thành công!");
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
        private bool IsValidVietnamesePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;

            // Xóa khoảng trắng, dấu chấm, dấu gạch
            phone = phone.Replace(" ", "").Replace(".", "").Replace("-", "");

            // Kiểm tra chỉ chứa số
            if (!phone.All(char.IsDigit))
                return false;

            // Độ dài hợp lệ (10 hoặc 11 số)
            if (phone.Length != 10 && phone.Length != 11)
                return false;

            // Bắt đầu bằng số 0 (Việt Nam)
            if (!phone.StartsWith("0"))
                return false;

            return true;
        }
        private bool IsValidFullName(string fullName, out string errorMsg)
        {
            errorMsg = "";

            if (string.IsNullOrWhiteSpace(fullName))
            {
                errorMsg = "Họ tên không được để trống.";
                return false;
            }

            fullName = fullName.Trim();

            // Độ dài hợp lệ
            if (fullName.Length < 2 || fullName.Length > 50)
            {
                errorMsg = "Họ tên phải từ 2 đến 50 ký tự.";
                return false;
            }

            // Không cho phép nhiều dấu cách liên tiếp
            if (fullName.Contains("  "))
            {
                errorMsg = "Họ tên không được chứa nhiều dấu cách liên tiếp.";
                return false;
            }

            // Kiểm tra ký tự hợp lệ: chữ cái, dấu cách, dấu tiếng Việt, dấu . hoặc -
            // Regex: ^[a-zA-ZÀ-ỹà-ỹ\s\.\-]+$
            var regex = new System.Text.RegularExpressions.Regex(@"^[a-zA-ZÀ-ỹà-ỹ\s]+$");
            if (!regex.IsMatch(fullName))
            {
                errorMsg = "Họ tên chỉ được chứa chữ cái, dấu cách.";
                return false;
            }

            // Không cho phép toàn dấu chấm/gạch/trống
            if (fullName.All(c => c == '.' || c == '-' || c == ' '))
            {
                errorMsg = "Họ tên không hợp lệ.";
                return false;
            }

            return true;
        }
    }
}
