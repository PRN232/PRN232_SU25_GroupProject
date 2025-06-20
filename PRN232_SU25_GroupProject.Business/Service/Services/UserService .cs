using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Users;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var exists = await _unitOfWork.UserRepository.Query()
                .AnyAsync(u => u.Email == request.Email);
            if (exists)
                throw new Exception("Email already in use.");

            var user = _mapper.Map<User>(request);
            user.IsActive = true;
            user.CreatedAt = DateTime.Now;

            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

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

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<bool> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
            if (user == null) return false;


            user.Email = request.Email;
            user.UserName = request.FullName;
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
