using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PRN232_SU25_GroupProject.DataAccess.DTOs.Common;

namespace PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetListById(int id);
        Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");
        Task<IEnumerable<T>> FindAllAsync(
                            Expression<Func<T, bool>> predicate,
                            string includeProperties = "");
        Task<PagedResult<T>> GetPagedListAsync(
            Expression<Func<T, bool>> filter,
            int pageIndex,
            int pageSize,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        Task<IEnumerable<T>> SearchAsync(Dictionary<string, object?> filters, List<string>? includes = null);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        IQueryable<T> Query();
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
