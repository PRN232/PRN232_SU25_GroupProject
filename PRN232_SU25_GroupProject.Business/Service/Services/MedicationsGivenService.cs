using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.MedicationGivens;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicationsGivenService : IMedicationsGivenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicationsGivenService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<MedicationGivenDto>>> GetAllMedicationsGivenAsync()
        {
            var medicationsGiven = await _unitOfWork.MedicationGivenRepository.GetAllAsync();
            var dtos = _mapper.Map<List<MedicationGivenDto>>(medicationsGiven);
            return ApiResponse<List<MedicationGivenDto>>.SuccessResult(dtos);
        }

        public async Task<ApiResponse<MedicationGivenDto>> GetMedicationsGivenByIdAsync(int id)
        {
            var medicationGiven = await _unitOfWork.MedicationGivenRepository.GetByIdAsync(id);
            if (medicationGiven == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("Không tìm thấy thông tin thuốc đã cung cấp.");

            var dto = _mapper.Map<MedicationGivenDto>(medicationGiven);
            return ApiResponse<MedicationGivenDto>.SuccessResult(dto);
        }

        public async Task<ApiResponse<MedicationGivenDto>> CreateMedicationsGivenAsync(CreateMedicationsGivenRequest request)
        {
            var medicationGiven = _mapper.Map<MedicationGiven>(request);
            var medicationIncident = await _unitOfWork.MedicalIncidentRepository
                .Query()
                .FirstOrDefaultAsync(mi => mi.Id == request.IncidentId);
            if (medicationIncident == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("IncidentId không khớp hoặc không tồn tại ");
            var medication = await _unitOfWork.MedicationRepository
                .Query()
                .FirstOrDefaultAsync(m => m.Id == request.MedicationId);
            if (medication == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("MedicationId không khớp hoặc không tồn tại ");
            medicationGiven.GivenAt = DateTime.UtcNow;
            await _unitOfWork.MedicationGivenRepository.AddAsync(medicationGiven);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<MedicationGivenDto>(medicationGiven);
            return ApiResponse<MedicationGivenDto>.SuccessResult(dto, "Tạo thông tin thuốc đã cung cấp thành công.");
        }

        public async Task<ApiResponse<MedicationGivenDto>> UpdateMedicationsGivenAsync(int id, UpdateMedicationsGivenRequest request)
        {
            var medicationGiven = await _unitOfWork.MedicationGivenRepository.GetByIdAsync(id);
            if (medicationGiven == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("Không tìm thấy thông tin thuốc đã cung cấp.");
            var medicationIncident = await _unitOfWork.MedicalIncidentRepository
                .Query()
                .FirstOrDefaultAsync(mi => mi.Id == request.IncidentId);
            if (medicationIncident == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("IncidentId không khớp hoặc không tồn tại ");
            var medication = await _unitOfWork.MedicationRepository
                .Query()
                .FirstOrDefaultAsync(m => m.Id == request.MedicationId);
            if (medication == null)
                return ApiResponse<MedicationGivenDto>.ErrorResult("MedicationId không khớp hoặc không tồn tại ");

            _mapper.Map(request, medicationGiven);
            _unitOfWork.MedicationGivenRepository.Update(medicationGiven);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<MedicationGivenDto>(medicationGiven);
            return ApiResponse<MedicationGivenDto>.SuccessResult(dto, "Cập nhật thông tin thuốc đã cung cấp thành công.");
        }

        public async Task<ApiResponse<bool>> DeleteMedicationsGivenAsync(int id)
        {
            var medicationGiven = await _unitOfWork.MedicationGivenRepository.GetByIdAsync(id);
            if (medicationGiven == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy thông tin thuốc đã cung cấp.");

            _unitOfWork.MedicationGivenRepository.Delete(medicationGiven);
            await _unitOfWork.SaveChangesAsync();
            return ApiResponse<bool>.SuccessResult(true, "Xóa thông tin thuốc đã cung cấp thành công.");
        }
    }
}
