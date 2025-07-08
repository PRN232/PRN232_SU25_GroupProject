using PRN232_SU25_GroupProject.DataAccess.Entities;

namespace PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentsByClassAsync(string className);
        Task<IEnumerable<Student>> GetStudentsByParentAsync(int parentId);
    }
}
