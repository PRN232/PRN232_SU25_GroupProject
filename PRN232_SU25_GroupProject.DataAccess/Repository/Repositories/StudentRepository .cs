using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;

namespace PRN232_SU25_GroupProject.DataAccess.Repository.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(SchoolMedicalDbContext context) : base(context) { }

        public async Task<IEnumerable<Student>> GetStudentsByClassAsync(string className)
        {
            return await _dbSet
                .Include(s => s.Parent)
                .Where(s => s.ClassName == className)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByParentAsync(int parentId)
        {
            return await _dbSet
                .Where(s => s.ParentId == parentId)
                .ToListAsync();
        }
    }
}