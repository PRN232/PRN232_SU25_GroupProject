using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalProfiles;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicalProfileService : IMedicalProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicalProfileService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<ApiResponse<MedicalProfileDto>> GetByStudentIdAsync(int studentId)
        {
            var profile = await _unitOfWork.GetRepository<MedicalProfile>()
                .Query()
                .Include(mp => mp.Allergies)
                .Include(mp => mp.ChronicDiseases)
                .Include(mp => mp.MedicalHistories)
                .Include(mp => mp.VaccinationRecords)
                .FirstOrDefaultAsync(mp => mp.StudentId == studentId);

            if (profile == null)
                return ApiResponse<MedicalProfileDto>.ErrorResult("Không tìm thấy hồ sơ sức khỏe.");

            var dto = _mapper.Map<MedicalProfileDto>(profile);

            // Lấy kết quả khám sức khỏe mới nhất (id lớn nhất hoặc ngày kiểm tra mới nhất)
            var lastCheckup = await _unitOfWork.HealthCheckupResultRepository
                .Query()
                .Where(x => x.StudentId == studentId)
                .OrderByDescending(x => x.CheckupDate)
                .FirstOrDefaultAsync();

            if (lastCheckup != null)
            {
                dto.Height = lastCheckup.Height;
                dto.Weight = lastCheckup.Weight;
                dto.BloodPressure = lastCheckup.BloodPressure;
                dto.VisionTest = lastCheckup.VisionTest;
                dto.HearingTest = lastCheckup.HearingTest;
                dto.GeneralHealth = lastCheckup.GeneralHealth;
                dto.RequiresFollowup = lastCheckup.RequiresFollowup;
                dto.Recommendations = lastCheckup.Recommendations;
                dto.LastCheckupDate = lastCheckup.CheckupDate;
                // KHÔNG map các trường id, campaignid, nursed, ...
            }

            return ApiResponse<MedicalProfileDto>.SuccessResult(dto);
        }


        public async Task<ApiResponse<MedicalProfileDto>> UpdateAsync(UpdateMedicalProfileRequest request)
        {
            var profile = await _unitOfWork.GetRepository<MedicalProfile>()
                .Query()
                .Include(mp => mp.Allergies)
                .Include(mp => mp.ChronicDiseases)
                .Include(mp => mp.MedicalHistories)
                .Include(mp => mp.VaccinationRecords)
                .FirstOrDefaultAsync(mp => mp.StudentId == request.StudentId);

            if (profile == null)
                return ApiResponse<MedicalProfileDto>.ErrorResult("Không tìm thấy hồ sơ sức khỏe.");

            _mapper.Map(request, profile);
            profile.LastUpdated = DateTime.UtcNow;

            _unitOfWork.GetRepository<MedicalProfile>().Update(profile);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<MedicalProfileDto>.SuccessResult(_mapper.Map<MedicalProfileDto>(profile), "Cập nhật hồ sơ sức khỏe thành công.");
        }
    }
}
