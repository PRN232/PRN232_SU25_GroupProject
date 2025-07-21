using PRN232_SU25_GroupProject.Business.DTOs.Parents;
using PRN232_SU25_GroupProject.Business.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IParentService
    {
        Task<ApiResponse<ParentDto>> GetParentByUserIdAsync(int userId);
        Task<ApiResponse<List<StudentDto>>> GetChildrenAsync(int parentId);
        Task<ApiResponse<bool>> UpdateParentInfoAsync(int userId, UpdateParentRequest request);
    }

}
