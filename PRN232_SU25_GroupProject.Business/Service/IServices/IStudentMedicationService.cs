﻿using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.StudentMedications;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IStudentMedicationService
    {
        Task<ApiResponse<StudentMedicationDto>> CreateStudentMedicationAsync(CreateStudentMedicationRequest request);
        Task<ApiResponse<StudentMedicationDto>> UpdateStudentMedicationAsync(int id, UpdateStudentMedicationRequest request);
        Task<ApiResponse<bool>> DeleteStudentMedicationAsync(int id);
        Task<ApiResponse<List<StudentMedicationDto>>> GetMedicationsByStudentAsync(int studentId);
        Task<ApiResponse<StudentMedicationDto>> ApproveStudentMedicationAsync(int id);
        Task<bool> CanParentAccessMedication(int parentId, int studentId);
    }
}
