using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.MedicalProfiles.Allergy;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class AllergyService : IAllergyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AllergyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<AllergyDto>>> GetAllergiesByProfileAsync(int medicalProfileId)
        {
            var allergies = await _unitOfWork.GetRepository<Allergy>()
                .Query()
                .Where(a => a.MedicalProfileId == medicalProfileId)
                .ToListAsync();

            return ApiResponse<List<AllergyDto>>.SuccessResult(_mapper.Map<List<AllergyDto>>(allergies));
        }

        public async Task<ApiResponse<AllergyDto>> GetAllergyByIdAsync(int allergyId)
        {
            var allergy = await _unitOfWork.GetRepository<Allergy>().GetByIdAsync(allergyId);
            if (allergy == null)
                return ApiResponse<AllergyDto>.ErrorResult("Không tìm thấy dị ứng.");
            return ApiResponse<AllergyDto>.SuccessResult(_mapper.Map<AllergyDto>(allergy));
        }

        public async Task<ApiResponse<AllergyDto>> CreateAllergyAsync(CreateAllergyRequest request)
        {
            // Optional: Check MedicalProfile exists
            var profile = await _unitOfWork.GetRepository<MedicalProfile>().GetByIdAsync(request.MedicalProfileId);
            if (profile == null)
                return ApiResponse<AllergyDto>.ErrorResult("Không tìm thấy hồ sơ sức khỏe.");

            var allergy = _mapper.Map<Allergy>(request);
            await _unitOfWork.GetRepository<Allergy>().AddAsync(allergy);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<AllergyDto>.SuccessResult(_mapper.Map<AllergyDto>(allergy), "Thêm dị ứng thành công!");
        }

        public async Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(UpdateAllergyRequest request)
        {
            var allergy = await _unitOfWork.GetRepository<Allergy>().GetByIdAsync(request.Id);
            if (allergy == null)
                return ApiResponse<AllergyDto>.ErrorResult("Không tìm thấy dị ứng.");
            _mapper.Map(request, allergy);
            _unitOfWork.GetRepository<Allergy>().Update(allergy);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<AllergyDto>.SuccessResult(_mapper.Map<AllergyDto>(allergy), "Cập nhật dị ứng thành công!");
        }

        public async Task<ApiResponse<bool>> DeleteAllergyAsync(int allergyId)
        {
            var allergy = await _unitOfWork.GetRepository<Allergy>().GetByIdAsync(allergyId);
            if (allergy == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy dị ứng.");
            _unitOfWork.GetRepository<Allergy>().Delete(allergy);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa dị ứng thành công!");
        }
    }

}
