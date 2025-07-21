using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Models.Common;
using PRN232_SU25_GroupProject.DataAccess.Repository.Interfaces;
using System.Linq.Expressions;

namespace PRN232_SU25_GroupProject.DataAccess.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SchoolMedicalDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SchoolMedicalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            // Ensure the previous task completes before proceeding
            var entity = await _dbSet.FindAsync(id);
            return entity;

        }

        public virtual async Task<IEnumerable<T>> GetListById(int id)
        {
            // Nên override ở repository cụ thể nếu có logic riêng
            throw new NotImplementedException("Override this method in a specific repository if needed.");
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            query = ApplyIncludes(query, includeProperties);
            return await query.ToListAsync();
        }

        public virtual async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            query = ApplyIncludes(query, includeProperties);
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet.Where(predicate);
            query = ApplyIncludes(query, includeProperties);
            return await query.ToListAsync();
        }

        public virtual async Task<PagedResult<T>> GetPagedListAsync(
            Expression<Func<T, bool>> filter,
            int pageIndex,
            int pageSize,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = _dbSet.Where(filter);
            query = ApplyIncludes(query, includeProperties);

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageIndex,
                PageSize = pageSize
            };
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(Dictionary<string, object?> filters, List<string>? includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include.Trim());
                }
            }

            foreach (var filter in filters)
            {
                if (filter.Value == null) continue;

                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.PropertyOrField(parameter, filter.Key);
                var constant = Expression.Constant(filter.Value);
                var equals = Expression.Equal(property, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(equals, parameter);

                query = query.Where(lambda);
            }

            return await query.ToListAsync();
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return predicate == null
                ? await _dbSet.CountAsync()
                : await _dbSet.CountAsync(predicate);
        }
        public virtual IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }

        protected IQueryable<T> ApplyIncludes(IQueryable<T> query, string includeProperties)
        {
            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                foreach (var include in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(include.Trim());
                }
            }

            return query;
        }
    }
}
