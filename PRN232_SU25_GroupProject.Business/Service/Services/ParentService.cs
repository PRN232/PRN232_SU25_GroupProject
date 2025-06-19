using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class ParentService : IParentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ParentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ParentDto> GetParentByUserIdAsync(int userId)
        {
            var parent = await _unitOfWork.ParentRepository
                .Query()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (parent == null) return null;

            var dto = _mapper.Map<ParentDto>(parent);
            dto.Children = _mapper.Map<List<StudentDto>>(parent.Children);

            return dto;
        }

        public async Task<List<StudentDto>> GetChildrenAsync(int parentId)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<bool> UpdateParentInfoAsync(UpdateParentRequest request)
        {
            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(request.Id);
            if (parent == null) return false;

            parent.FullName = request.FullName;
            parent.PhoneNumber = request.PhoneNumber;
            parent.Address = request.Address;

            _unitOfWork.ParentRepository.Update(parent);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
