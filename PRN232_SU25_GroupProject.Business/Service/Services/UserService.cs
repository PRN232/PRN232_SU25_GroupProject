using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.Users;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var exists = await _userManager.FindByEmailAsync(request.Email);
            if (exists != null)
                throw new Exception("Email already in use.");

            var user = _mapper.Map<User>(request);
            user.UserName = request.Username;
            user.Email = request.Email;
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create user: " + string.Join("; ", result.Errors.Select(e => e.Description)));
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public async Task<List<UserDto>> GetUsersByRoleAsync(UserRole role)
        {
            var users = await _unitOfWork.UserRepository.Query()
                .Where(u => u.Role == role)
                .ToListAsync();

            var userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                var dto = _mapper.Map<UserDto>(user);

                switch (role)
                {
                    case UserRole.Parent:
                        var parent = await _unitOfWork.ParentRepository
                            .Query().FirstOrDefaultAsync(p => p.UserId == user.Id);
                        if (parent != null)
                            dto.UserId = parent.Id;
                        break;

                    case UserRole.SchoolNurse:
                        var nurse = await _unitOfWork.SchoolNurseRepository
                            .Query().FirstOrDefaultAsync(n => n.UserId == user.Id);
                        if (nurse != null)
                            dto.UserId = nurse.Id;
                        break;
                }

                userDtos.Add(dto);
            }

            return userDtos;
        }


        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null) return false;


            user.Email = request.Email;
            user.UserName = request.Username;
            user.PhoneNumber = request.PhoneNumber;
            user.IsActive = request.IsActive;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.IsActive = false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
