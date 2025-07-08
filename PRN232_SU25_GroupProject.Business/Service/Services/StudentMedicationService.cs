using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.Business.Service.IServices;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository;

namespace PRN232_SU25_GroupProject.Business.Service.Services
{
    public class StudentMedicationService : IStudentMedicationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentMedicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Create new student medication
        public async Task<ApiResponse<StudentMedicationDto>> CreateStudentMedicationAsync(CreateStudentMedicationRequest request)
        {
            // Validate Student exists
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Không tìm thấy học sinh.");

            // Validate Parent for authorization
            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId);
            if (parent == null)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Học sinh không có phụ huynh hoặc phụ huynh không hợp lệ.");

            // Check if the medication already exists for this student
            var medicationExist = await _unitOfWork.StudentMedicationRepository.Query()
                .AnyAsync(m => m.StudentId == request.StudentId && m.MedicationName == request.MedicationName && m.StartDate <= request.EndDate && m.EndDate >= request.StartDate);

            if (medicationExist)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Đơn thuốc đã tồn tại cho học sinh này trong khoảng thời gian này.");

            // Create the medication record
            var medication = _mapper.Map<StudentMedication>(request);
            medication.IsApproved = false; // By default, the medication will not be approved
            await _unitOfWork.StudentMedicationRepository.AddAsync(medication);
            await _unitOfWork.SaveChangesAsync();

            // Map DTO to return
            var dto = _mapper.Map<StudentMedicationDto>(medication);

            return ApiResponse<StudentMedicationDto>.SuccessResult(dto, "Đơn thuốc đã được tạo thành công.");
        }

        // Update student medication
        public async Task<ApiResponse<StudentMedicationDto>> UpdateStudentMedicationAsync(int id, UpdateStudentMedicationRequest request)
        {
            // Find the medication to be updated
            var medication = await _unitOfWork.StudentMedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Không tìm thấy đơn thuốc.");

            // Check if the student ID matches with the medication student
            if (medication.StudentId != request.StudentId)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Học sinh không hợp lệ cho đơn thuốc này.");

            // Validate Parent for authorization (same logic as in create)
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(request.StudentId);
            var parent = await _unitOfWork.ParentRepository.GetByIdAsync(student.ParentId);
            if (parent == null)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Phụ huynh không hợp lệ.");

            // Update medication details
            medication.MedicationName = request.MedicationName;
            medication.Dosage = request.Dosage;
            medication.Instructions = request.Instructions;
            medication.AdministrationTime = request.AdministrationTime;
            medication.StartDate = request.StartDate;
            medication.EndDate = request.EndDate;
            medication.IsApproved = request.IsApproved;

            // Save changes
            _unitOfWork.StudentMedicationRepository.Update(medication);
            await _unitOfWork.SaveChangesAsync();

            // Map DTO to return
            var dto = _mapper.Map<StudentMedicationDto>(medication);

            return ApiResponse<StudentMedicationDto>.SuccessResult(dto, "Đơn thuốc đã được cập nhật thành công.");
        }

        // Delete student medication
        public async Task<ApiResponse<bool>> DeleteStudentMedicationAsync(int id)
        {
            // Find the medication to be deleted
            var medication = await _unitOfWork.StudentMedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<bool>.ErrorResult("Không tìm thấy đơn thuốc.");

            // Check if the student ID matches with the medication student
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(medication.StudentId);
            if (student == null)
                return ApiResponse<bool>.ErrorResult("Học sinh không hợp lệ.");

            // Delete the medication record
            _unitOfWork.StudentMedicationRepository.Delete(medication);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponse<bool>.SuccessResult(true, "Đơn thuốc đã được xóa thành công.");
        }

        // Get all medications for a student
        public async Task<ApiResponse<List<StudentMedicationDto>>> GetMedicationsByStudentAsync(int studentId)
        {
            // Validate if student exists
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);
            if (student == null)
                return ApiResponse<List<StudentMedicationDto>>.ErrorResult("Không tìm thấy học sinh.");

            // Fetch all medications for the student
            var medications = await _unitOfWork.StudentMedicationRepository.Query()
                .Where(m => m.StudentId == studentId)
                .ToListAsync();

            // Map to DTOs
            var dtos = _mapper.Map<List<StudentMedicationDto>>(medications);
            return ApiResponse<List<StudentMedicationDto>>.SuccessResult(dtos);
        }

        // Approve medication
        public async Task<ApiResponse<StudentMedicationDto>> ApproveStudentMedicationAsync(int id)
        {
            var medication = await _unitOfWork.StudentMedicationRepository.GetByIdAsync(id);
            if (medication == null)
                return ApiResponse<StudentMedicationDto>.ErrorResult("Không tìm thấy đơn thuốc.");

            // Approve the medication
            medication.IsApproved = true;
            _unitOfWork.StudentMedicationRepository.Update(medication);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<StudentMedicationDto>(medication);
            return ApiResponse<StudentMedicationDto>.SuccessResult(dto, "Đơn thuốc đã được phê duyệt.");
        }
        public async Task<bool> CanParentAccessMedication(int userId, int studentId)
        {
            var parentId = await _unitOfWork.ParentRepository.Query()
                .Where(p => p.UserId == userId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
            // Check if the parent is the parent of the student.
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(studentId);

            if (student == null)
            {
                return false; // If the student does not exist, return false.
            }
            // Check if the parentId matches the student's ParentId.
            return student.ParentId == parentId;
        }

    }
}
