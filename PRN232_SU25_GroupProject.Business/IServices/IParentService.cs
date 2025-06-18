using PRN232_SU25_GroupProject.DataAccess.DTOs;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Parents;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.Business.IServices
{
    public interface IParentService
    {
        Task<Parent> GetParentByUserIdAsync(int userId);
        Task<List<Student>> GetChildrenAsync(int parentId);
        Task<bool> UpdateParentInfoAsync(UpdateParentRequest request);
    }
}
