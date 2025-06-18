using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Students;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IStudentService
    {
        Task<Student> CreateStudentAsync(CreateStudentRequest request);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Student>> GetStudentsByClassAsync(string className);
        Task<List<Student>> GetStudentsByParentAsync(int parentId);
        Task<bool> UpdateStudentAsync(UpdateStudentRequest request);
    }
}
