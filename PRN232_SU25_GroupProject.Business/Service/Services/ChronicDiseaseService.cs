using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.ChronicDisease;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class ChronicDiseaseService : IChronicDiseaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChronicDiseaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ChronicDiseaseDto>>> GetChronicDiseasesByProfileAsync(int medicalProfileId)
        {
            var diseases = await _unitOfWork.GetRepository<ChronicDisease>()
                .Query()
                .Where(d => d.MedicalProfileId == medicalProfileId)
                .ToListAsync();

            return ApiResponse<List<ChronicDiseaseDto>>.SuccessResult(_mapper.Map<List<ChronicDiseaseDto>>(diseases));
        }

        public async Task<ApiResponse<ChronicDiseaseDto>> GetChronicDiseaseByIdAsync(int chronicDiseaseId)
        {
            var disease = await _unitOfWork.GetRepository<ChronicDisease>().GetByIdAsync(chronicDiseaseId);
            if (disease == null)
                return ApiResponse<ChronicDiseaseDto>.ErrorResult("Không tìm thấy bệnh mạn tính.");
            return ApiResponse<ChronicDiseaseDto>.SuccessResult(_mapper.Map<ChronicDiseaseDto>(disease));
        }

        public async Task<ApiResponse<ChronicDiseaseDto>> CreateChronicDiseaseAsync(CreateChronicDiseaseRequest request)
        {
            // Optional: Check MedicalProfile exists
            var profile = await _unitOfWork.GetRepository<MedicalProfile>().GetByIdAsync(request.MedicalProfileId);
            if (profile == null)
                return ApiResponse<ChronicDiseaseDto>.ErrorResult("Không tìm thấy hồ sơ sức khỏe.");

            var disease = _mapper.Map<ChronicDisease>(request);
            await _unitOfWork.GetRepository<ChronicDisease>().AddAsync(disease);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<ChronicDiseaseDto>.SuccessResult(_mapper.Map<ChronicDiseaseDto>(disease), "Thêm bệnh mạn tính thành công!");
        }

        public async Task<ApiResponse<ChronicDiseaseDto>> UpdateChronicDiseaseAsync(UpdateChronicDiseaseRequest request)
        {
            var disease = await _unitOfWork.GetRepository<ChronicDisease>().GetByIdAsync(request.Id);
            if (disease == null)
                return ApiResponse<ChronicDiseaseDto>.ErrorResult("Không tìm thấy bệnh mạn tính.");
            _mapper.Map(request, disease);
            _unitOfWork.GetRepository<ChronicDisease>().Update(disease);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<ChronicDiseaseDto>.SuccessResult(_mapper.Map<ChronicDiseaseDto>(disease), "Cập nhật bệnh mạn tính thành công!");
        }

        public async Task<ApiResponse<bool>> DeleteChronicDiseaseAsync(int chronicDiseaseId)
        {
            var disease = await _unitOfWork.GetRepository<ChronicDisease>().GetByIdAsync(chronicDiseaseId);
            if (disease == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy bệnh mạn tính.");
            _unitOfWork.GetRepository<ChronicDisease>().Delete(disease);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa bệnh mạn tính thành công!");
        }
    }

}
