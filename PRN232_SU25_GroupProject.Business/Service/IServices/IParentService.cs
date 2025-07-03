using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IParentService
    {
        Task<ApiResponse<ParentDto>> GetParentByUserIdAsync(int userId);
        Task<ApiResponse<List<StudentDto>>> GetChildrenAsync(int parentId);
        Task<ApiResponse<bool>> UpdateParentInfoAsync(UpdateParentRequest request);
    }

}
