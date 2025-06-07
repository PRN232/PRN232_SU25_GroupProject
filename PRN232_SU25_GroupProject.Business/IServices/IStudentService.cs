using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IStudentService
    {
        Task<PagedResult<Student>> GetStudentsAsync(StudentFilterRequest filter);
        Task<Student> GetStudentByIdAsync(int studentId);
        Task<Student> GetStudentByCodeAsync(string studentCode);
        Task<Student> CreateStudentAsync(CreateStudentRequest request);
        Task<Student> UpdateStudentAsync(int studentId, UpdateStudentRequest request);
        Task<bool> DeleteStudentAsync(int studentId);
        Task<IEnumerable<Student>> GetStudentsByClassAsync(string className, int schoolId);
        Task<IEnumerable<Student>> GetStudentsByParentAsync(int parentId);
        Task<bool> UploadStudentPhotoAsync(int studentId, IFormFile photo);
    }
}
