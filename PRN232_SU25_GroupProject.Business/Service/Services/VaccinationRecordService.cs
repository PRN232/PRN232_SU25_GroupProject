using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Vaccinations;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class VaccinationRecordService : IVaccinationRecordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VaccinationRecordService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<VaccinationRecordDto>> CreateRecordAsync(CreateVaccinationRecordRequest request)
        {
            var entity = _mapper.Map<VaccinationRecord>(request);
            await _unitOfWork.VaccinationRecordRepository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Lấy thông tin phụ để trả về
            var dto = await MapVaccinationRecordDtoAsync(entity);
            return ApiResponse<VaccinationRecordDto>.SuccessResult(dto, "Ghi nhận tiêm chủng thành công!");
        }

        public async Task<ApiResponse<VaccinationRecordDto>> GetRecordByIdAsync(int id)
        {
            var record = await _unitOfWork.VaccinationRecordRepository.GetByIdAsync(id);
            if (record == null)
                return ApiResponse<VaccinationRecordDto>.ErrorResult("Không tìm thấy phiếu tiêm chủng.");

            var dto = await MapVaccinationRecordDtoAsync(record);
            return ApiResponse<VaccinationRecordDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<List<VaccinationRecordDto>>> GetRecordsByCampaignAsync(int campaignId)
        {
            var records = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => r.CampaignId == campaignId)
                .ToListAsync();

            var dtos = new List<VaccinationRecordDto>();
            foreach (var record in records)
                dtos.Add(await MapVaccinationRecordDtoAsync(record));

            return ApiResponse<List<VaccinationRecordDto>>.SuccessResult(dtos);
        }

        public async Task<ApiResponse<List<VaccinationRecordDto>>> GetRecordsByStudentAsync(int studentId)
        {
            var records = await _unitOfWork.VaccinationRecordRepository.Query()
                .Where(r => r.StudentId == studentId)
                .ToListAsync();

            var dtos = new List<VaccinationRecordDto>();
            foreach (var record in records)
                dtos.Add(await MapVaccinationRecordDtoAsync(record));

            return ApiResponse<List<VaccinationRecordDto>>.SuccessResult(dtos);
        }

        public async Task<ApiResponse<VaccinationRecordDto>> UpdateRecordAsync(int id, UpdateVaccinationRecordRequest request)
        {
            var record = await _unitOfWork.VaccinationRecordRepository.GetByIdAsync(id);
            if (record == null)
                return ApiResponse<VaccinationRecordDto>.ErrorResult("Không tìm thấy phiếu tiêm chủng.");

            _mapper.Map(request, record);
            _unitOfWork.VaccinationRecordRepository.Update(record);
            await _unitOfWork.SaveChangesAsync();

            var dto = await MapVaccinationRecordDtoAsync(record);
            return ApiResponse<VaccinationRecordDto>.SuccessResult(dto, "Cập nhật thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteRecordAsync(int id)
        {
            var record = await _unitOfWork.VaccinationRecordRepository.GetByIdAsync(id);
            if (record == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy phiếu tiêm chủng.");

            _unitOfWork.VaccinationRecordRepository.Delete(record);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa phiếu tiêm chủng thành công.");
        }


        private async Task<VaccinationRecordDto> MapVaccinationRecordDtoAsync(VaccinationRecord record)
        {
            var dto = _mapper.Map<VaccinationRecordDto>(record);

            var student = await _unitOfWork.StudentRepository.GetByIdAsync(record.StudentId);
            var nurse = await _unitOfWork.SchoolNurseRepository.GetByIdAsync(record.NurseId);
            var campaign = await _unitOfWork.VaccinationCampaignRepository.GetByIdAsync(record.CampaignId);

            dto.StudentName = student?.FullName;
            dto.StudentCode = student?.StudentCode;
            dto.NurseName = nurse?.FullName;
            dto.CampaignName = campaign?.CampaignName;

            return dto;
        }
    }


}
