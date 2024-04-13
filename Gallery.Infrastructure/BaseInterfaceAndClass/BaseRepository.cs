using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Gallery.Infrastructure.BaseInterfaceAndClass
{
    public class BaseRepository : IBaseRepository { }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbSet<T> _readContext;
        protected readonly DbSet<T> _writeContext;
        private readonly GalleryDbContext _dbContext;
        private bool disposedValue;

        public BaseRepository(GalleryDbContext readContext, GalleryDbContext writeContext)
        {
            _readContext = readContext.Set<T>();
            _writeContext = writeContext.Set<T>();
            _dbContext = writeContext;
        }

        public IQueryable<T> GetAll(bool readOnly, params Expression<Func<T, object>>[] includes) => includes.Aggregate((readOnly ? _readContext.AsQueryable().AsNoTracking() : _writeContext.AsQueryable()), (current, expression) => current.Include(expression));

        public IQueryable<T> GetAll(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = (readOnly ? _readContext.AsQueryable().AsNoTracking() : _writeContext.AsQueryable()).AsQueryable();
            if (func != null)
            {
                result = result.Where(func).AsQueryable();
            }

            return includes.Aggregate(result, (current, expression) => current.Include(expression));
        }

        public IQueryable<TResult> GetAllThenInclude<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true, bool readOnly = true)
        {
            IQueryable<T> query;

            if (readOnly == true)
            {
                query = _readContext;
            }
            else
            {
                query = _writeContext;
            }

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (query == null || !query.Any())
            {
                return null;
            }

            return query.Select(selector);
        }

        public bool Any(Expression<Func<T, bool>> func) => _readContext.Any(func);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> func) => await _readContext.AnyAsync(func);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes)
        {
            if (includes == null)
                return await AnyAsync(func);
            else
                return await GetAll(true, func, includes).AnyAsync();
        }

        public async Task<bool> AnyAsyncWithThenInclude(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            if (include == null)
                return await AnyAsync(predicate);
            else
                return await GetAllThenInclude(x => x, predicate, include).AnyAsync();
        }

        public int Count(Expression<Func<T, bool>> func = null)
        {
            if (func == null)
            {
                return _readContext.Count();
            }
            return _readContext.Count(func);
        }

        public T? First(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = (readOnly ? _readContext.AsQueryable().AsNoTracking() : _writeContext.AsQueryable()).AsQueryable();
            if (func != null)
            {
                result = result.Where(func).AsQueryable();
            }
            result = includes.Aggregate(result, (current, expression) => current.Include(expression));
            return result.FirstOrDefault();
        }

        public async Task<T?> FirstAsync(bool readOnly, Expression<Func<T, bool>> func, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> result = (readOnly ? _readContext.AsQueryable().AsNoTracking() : _writeContext.AsQueryable()).AsQueryable();
            if (func != null)
            {
                result = result.Where(func).AsQueryable();
            }
            result = includes.Aggregate(result, (current, expression) => current.Include(expression));
            return await Task.Run(() => result.FirstOrDefault());
            //return await result.FirstOrDefaultAsync();
        }

        public T? FindById(bool readOnly = true, params object[]? keys) => readOnly == true ? _readContext.Find(keys) : _writeContext.Find(keys);

        public async Task<T?> FindByIdAsync(bool readOnly = true, params object[]? keys) => readOnly == true ? await _readContext.FindAsync(keys) : await _writeContext.FindAsync(keys);

        public T Update(T entity) => _writeContext.Update(entity).Entity;

        public void UpdateRange(IEnumerable<T> entities) => _writeContext.UpdateRange(entities);

        public T Add(T entity) => _writeContext.Add(entity).Entity;

        public async Task<T> AddAsync(T entity) => (await _writeContext.AddAsync(entity)).Entity;

        public void AddRangeEntity(IEnumerable<T> entities) => _writeContext.AddRange(entities);

        public async Task AddRangeEntityAsync(IEnumerable<T> entities) => await _writeContext.AddRangeAsync(entities);

        public void Delete(T entity) => _writeContext.Remove(entity);

        public void DeleteRange(IEnumerable<T> entities) => _writeContext.RemoveRange(entities);

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~BaseRepository() => Dispose(disposing: false);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public int Commit() => _dbContext.SaveChanges();
        public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync();
    }
}