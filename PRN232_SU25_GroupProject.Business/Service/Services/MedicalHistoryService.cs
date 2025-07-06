using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles.MedicalHistory;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicalHistoryService : IMedicalHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MedicalHistoryDto>>> GetMedicalHistoriesByProfileAsync(int medicalProfileId)
        {
            var histories = await _unitOfWork.GetRepository<MedicalHistory>()
                .Query()
                .Where(h => h.MedicalProfileId == medicalProfileId)
                .ToListAsync();

            return ApiResponse<List<MedicalHistoryDto>>.SuccessResult(_mapper.Map<List<MedicalHistoryDto>>(histories));
        }

        public async Task<ApiResponse<MedicalHistoryDto>> GetMedicalHistoryByIdAsync(int medicalHistoryId)
        {
            var history = await _unitOfWork.GetRepository<MedicalHistory>().GetByIdAsync(medicalHistoryId);
            if (history == null)
                return ApiResponse<MedicalHistoryDto>.ErrorResult("Không tìm thấy lịch sử bệnh án.");
            return ApiResponse<MedicalHistoryDto>.SuccessResult(_mapper.Map<MedicalHistoryDto>(history));
        }

        public async Task<ApiResponse<MedicalHistoryDto>> CreateMedicalHistoryAsync(CreateMedicalHistoryRequest request)
        {
            // Optional: Check MedicalProfile exists
            var profile = await _unitOfWork.GetRepository<MedicalProfile>().GetByIdAsync(request.MedicalProfileId);
            if (profile == null)
                return ApiResponse<MedicalHistoryDto>.ErrorResult("Không tìm thấy hồ sơ sức khỏe.");

            var history = _mapper.Map<MedicalHistory>(request);
            await _unitOfWork.GetRepository<MedicalHistory>().AddAsync(history);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<MedicalHistoryDto>.SuccessResult(_mapper.Map<MedicalHistoryDto>(history), "Thêm lịch sử bệnh án thành công!");
        }

        public async Task<ApiResponse<MedicalHistoryDto>> UpdateMedicalHistoryAsync(UpdateMedicalHistoryRequest request)
        {
            var history = await _unitOfWork.GetRepository<MedicalHistory>().GetByIdAsync(request.Id);
            if (history == null)
                return ApiResponse<MedicalHistoryDto>.ErrorResult("Không tìm thấy lịch sử bệnh án.");
            _mapper.Map(request, history);
            _unitOfWork.GetRepository<MedicalHistory>().Update(history);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<MedicalHistoryDto>.SuccessResult(_mapper.Map<MedicalHistoryDto>(history), "Cập nhật lịch sử bệnh án thành công!");
        }

        public async Task<ApiResponse<bool>> DeleteMedicalHistoryAsync(int medicalHistoryId)
        {
            var history = await _unitOfWork.GetRepository<MedicalHistory>().GetByIdAsync(medicalHistoryId);
            if (history == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy lịch sử bệnh án.");
            _unitOfWork.GetRepository<MedicalHistory>().Delete(history);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa lịch sử bệnh án thành công!");
        }
    }

}
