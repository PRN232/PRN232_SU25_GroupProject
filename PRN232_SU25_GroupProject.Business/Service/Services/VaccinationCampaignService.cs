using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Enums;
using PRN232_SU25_GroupProject.DataAccess.Repository.Repositories;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class VaccinationCampaignService : IVaccinationCampaignService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VaccinationCampaignService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<VaccinationCampaignDto>>> GetAllCampaignsAsync()
        {
            var campaigns = await _unitOfWork.VaccinationCampaignRepository.GetAllAsync();
            var dtos = _mapper.Map<List<VaccinationCampaignDto>>(campaigns);
            return ApiResponse<List<VaccinationCampaignDto>>.SuccessResult(dtos);
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> GetCampaignByIdAsync(int id)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<VaccinationCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");
            var dto = _mapper.Map<VaccinationCampaignDto>(campaign);
            return ApiResponse<VaccinationCampaignDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> CreateCampaignAsync(CreateVaccinationCampaignRequest request)
        {
            var entity = _mapper.Map<VaccinationCampaign>(request);
            entity.Status = VaccinationStatus.Planned;
            entity.ScheduledDate = DateTime.UtcNow;
            await _unitOfWork.VaccinationCampaignRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<VaccinationCampaignDto>(entity);
            return ApiResponse<VaccinationCampaignDto>.SuccessResult(dto, "Tạo chiến dịch tiêm chủng thành công.");
        }

        public async Task<ApiResponse<VaccinationCampaignDto>> UpdateCampaignAsync(int recordId, UpdateVaccinationCampaignRequest request)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(recordId);
            if (campaign == null)
                return ApiResponse<VaccinationCampaignDto>.ErrorResult("Không tìm thấy chiến dịch.");
            _mapper.Map(request, campaign);
            _unitOfWork.VaccinationCampaignRepository.Update(campaign);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<VaccinationCampaignDto>(campaign);
            return ApiResponse<VaccinationCampaignDto>.SuccessResult(dto, "Cập nhật thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteCampaignAsync(int id)
        {
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(id);
            if (campaign == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy chiến dịch.");
            _unitOfWork.VaccinationCampaignRepository.Delete(campaign);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa chiến dịch thành công.");
        }

        public async Task<ApiResponse<List<StudentDto>>> GetVaccinatedStudentsAsync(int campaignId)
        {
            // Lấy các VaccinationRecord theo campaign
            var records = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var studentIds = records.Select(r => r.StudentId).Distinct().ToList();

            var students = await _unitOfWork.StudentRepository.Query()
    .Include(s => s.Parent)
    .Include(s => s.MedicalProfile)
    .Where(s => studentIds.Contains(s.Id))
    .ToListAsync();


            var dtos = _mapper.Map<List<StudentDto>>(students);
            return ApiResponse<List<StudentDto>>.SuccessResult(dtos);
        }
    }

}
