using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IParentService
    {
        Task<ParentDto> GetParentByUserIdAsync(int userId);
        Task<List<StudentDto>> GetChildrenAsync(int parentId);
        Task<bool> UpdateParentInfoAsync(UpdateParentRequest request);
    }
}
