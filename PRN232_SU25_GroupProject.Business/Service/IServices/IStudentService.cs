using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse<StudentDto>> CreateStudentAsync(CreateStudentRequest request);
        Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id);
        Task<ApiResponse<List<StudentDto>>> GetStudentsByClassAsync(string className);
        Task<ApiResponse<List<StudentDto>>> GetStudentsByParentAsync(int parentId);
        Task<ApiResponse<bool>> UpdateStudentAsync(int id, UpdateStudentRequest request);
    }

}
