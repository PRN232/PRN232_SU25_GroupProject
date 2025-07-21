using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.DTOs.Medications;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class MedicationService : IMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MedicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Add Medication
        public async Task<ApiResponse<MedicationDto>> AddMedicationAsync(AddMedicationRequest request)
        {
            // Check if a medication with the same name already exists
            var existingMedication = await _unitOfWork.MedicationRepository
                .Query()
                .FirstOrDefaultAsync(m => m.Name == request.Name);

            if (existingMedication != null)
                return ApiResponse<MedicationDto>.ErrorResult("Thuốc này đã tồn tại trong hệ thống.");

            var medication = _mapper.Map<Medication>(request);
            await _unitOfWork.MedicationRepository.AddAsync(medication);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<MedicationDto>(medication);
            return ApiResponse<MedicationDto>.SuccessResult(dto, "Thêm thuốc thành công.");
        }

        // Update Medication
        public async Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int id, UpdateMedicationRequest request)
        {
            var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<MedicationDto>.ErrorResult("Không tìm thấy thuốc.");

            //Update medication details

            //_mapper.Map(request, medication);
            medication.Name = request.Name;
            medication.Description = request.Description;
            medication.Type = request.Type;
            medication.StockQuantity = request.StockQuantity;
            medication.ExpiryDate = request.ExpiryDate;
            medication.StorageInstructions = request.StorageInstructions;
            _unitOfWork.MedicationRepository.Update(medication);
            await _unitOfWork.SaveChangesAsync();

            //var dto = _mapper.Map<MedicationDto>(medication);
            var dto = new MedicationDto
            {
                Id = medication.Id,
                Name = medication.Name,
                Description = medication.Description,
                Type = medication.Type,
                StockQuantity = medication.StockQuantity,
                ExpiryDate = medication.ExpiryDate,
                StorageInstructions = medication.StorageInstructions
            };




            return ApiResponse<MedicationDto>.SuccessResult(dto, "Cập nhật thuốc thành công.");
        }

        // Delete Medication
        public async Task<ApiResponse<bool>> DeleteMedicationAsync(int id)
        {
            // Tìm kiếm thuốc
            var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy thuốc.");

            // Kiểm tra và xóa các bản ghi tham chiếu trong MedicationsGiven
            var medicationsGiven = await _unitOfWork.MedicationGivenRepository.Query()
                .Where(mg => mg.MedicationId == id)
                .ToListAsync();

            if (medicationsGiven.Any())
            {
                var medicationGivenIds = medicationsGiven.Select(mg => mg.Id).ToList(); // Lấy danh sách các Id của medicationsGiven
                var idsString = string.Join(", ", medicationGivenIds); // Chuyển danh sách Id thành chuỗi

                // Trả về lỗi nếu thuốc đang được sử dụng trong MedicationsGiven
                return ApiResponse<bool>.ErrorResult($"Không thể xóa thuốc do thuốc đó đang được sử dụng ở MedicationsGiven với các Id: {idsString}");
            }

            // Nếu không có bản ghi nào trong MedicationsGiven tham chiếu đến thuốc, tiến hành xóa thuốc
            _unitOfWork.MedicationRepository.Delete(medication);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResult(true, "Xóa thuốc thành công.");
        }



        // Get All Medications
        public async Task<ApiResponse<List<MedicationDto>>> GetAllMedicationsAsync()
        {
            var medications = await _unitOfWork.MedicationRepository.GetAllAsync();
            var dtos = _mapper.Map<List<MedicationDto>>(medications);
            return ApiResponse<List<MedicationDto>>.SuccessResult(dtos);
        }

        // Get Medication By Id
        public async Task<ApiResponse<MedicationDto>> GetMedicationByIdAsync(int id)
        {
            var medication = await _unitOfWork.MedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<MedicationDto>.ErrorResult("Không tìm thấy thuốc.");

            var dto = _mapper.Map<MedicationDto>(medication);
            return ApiResponse<MedicationDto>.SuccessResult(dto);
        }
    }
}
