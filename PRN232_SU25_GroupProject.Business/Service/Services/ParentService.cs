using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Repository;

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

        public async Task<ApiResponse<ParentDto>> GetParentByUserIdAsync(int userId)
        {
            var parent = await _unitOfWork.ParentRepository
                .Query()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (parent == null)
                return ApiResponse<ParentDto>.ErrorResult("Không tìm thấy thông tin phụ huynh.");
            var user = await _unitOfWork.UserRepository.Query().FirstOrDefaultAsync(c => c.Id == userId);
            var dto = _mapper.Map<ParentDto>(parent);
            dto.PhoneNumber = parent.PhoneNumber;
            dto.FullName = parent.FullName;
            dto.Address = parent.Address;
            dto.Email = user.Email;

            return ApiResponse<ParentDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<List<StudentDto>>> GetChildrenAsync(int parentId)
        {
            var students = await _unitOfWork.StudentRepository
                .Query()
                .Where(s => s.ParentId == parentId)
                .ToListAsync();

            return ApiResponse<List<StudentDto>>.SuccessResult(_mapper.Map<List<StudentDto>>(students));
        }

        public async Task<ApiResponse<bool>> UpdateParentInfoAsync(int userId, UpdateParentRequest request)
        {
            var parent = await _unitOfWork.ParentRepository
                .Query()
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (parent == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy thông tin phụ huynh.");

            _mapper.Map(request, parent);

            _unitOfWork.ParentRepository.Update(parent);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResult(true, "Cập nhật thông tin thành công.");
        }
    }
}
