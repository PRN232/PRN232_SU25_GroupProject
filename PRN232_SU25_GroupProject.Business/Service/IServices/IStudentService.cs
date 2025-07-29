using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse<StudentDto>> CreateStudentAsync(CreateStudentRequest request);
        Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id);
        Task<ApiResponse<List<StudentDto>>> GetStudentsByClassAsync(string className);
        Task<ApiResponse<List<StudentDto>>> GetStudentsByParentAsync(int parentId);
        Task<ApiResponse<bool>> UpdateStudentAsync(int id, UpdateStudentRequest request);
        Task<ApiResponse<List<StudentDto>>> GetAllStudentsAsync();
        /// <summary>
        /// Lấy danh sách các lớp (chỉ tên lớp) và số lượng học sinh từng lớp
        /// </summary>
        Task<ApiResponse<List<ClassSummaryDto>>> GetClassSummariesAsync();


    }

}
