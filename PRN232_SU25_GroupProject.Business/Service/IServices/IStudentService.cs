using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.Service.IServices
{
    public interface IStudentService
    {
        Task<StudentDto> CreateStudentAsync(CreateStudentRequest request);
        Task<StudentDto> GetStudentByIdAsync(int id);
        Task<List<StudentDto>> GetStudentsByClassAsync(string className);
        Task<List<StudentDto>> GetStudentsByParentAsync(int parentId);
        Task<bool> UpdateStudentAsync(UpdateStudentRequest request);
    }
}
