using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IStudentParentService
    {
        Task<IEnumerable<StudentParent>> GetStudentParentsAsync(int studentId);
        Task<StudentParent> CreateStudentParentAsync(CreateStudentParentRequest request);
        Task<bool> DeleteStudentParentAsync(int studentParentId);
        Task<bool> SetPrimaryParentAsync(int studentParentId);
        Task<IEnumerable<Student>> GetChildrenByParentAsync(int parentId);
    }

}
