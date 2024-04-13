using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Gallery.Infrastructure.BaseInterfaceAndClass
{
    public interface IBaseRepository { }

    public interface IBaseRepository<T> : IBaseRepository where T : class
    {
        IQueryable<T> GetAll(bool readOnly, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetAll(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes);
        IQueryable<TResult> GetAllThenInclude<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool readOnly = true);
        bool Any(Expression<Func<T, bool>> func);
        Task<bool> AnyAsync(Expression<Func<T, bool>> func);
        Task<bool> AnyAsync(Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes);
        Task<bool> AnyAsyncWithThenInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T? First(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes);
        Task<T?> FirstAsync(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes);
        T Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        void AddRangeEntity(IEnumerable<T> entities);
        Task AddRangeEntityAsync(IEnumerable<T> entities);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        T? FindById(bool readOnly = true, params object[]? keys);
        Task<T?> FindByIdAsync(bool readOnly = true, params object[]? keys);
        int Commit();
        Task<int> CommitAsync();
    }
}