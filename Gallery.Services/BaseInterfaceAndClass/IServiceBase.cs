using Gallery.DTO.BaseInterfaceAndClass;
using Gallery.Models.BaseEntityModel;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Gallery.Services.BaseInterfaceAndClass
{
    public interface IServiceBase { }
    public interface IMarker { }
    public interface IServiceBase<E, D, K> : IServiceBase where E : BaseEntity<K> where D : IBaseDTO where K : struct
    {
        D TranslateToDTO(E entity);
        E TranslateToEntity(D model);
        Task<bool> AnyAsync(Expression<Func<E, bool>> func);
        Task<E> GetAsync(bool readOnly, Expression<Func<E, bool>> func, params Expression<Func<E, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<E, bool>> func, params Expression<Func<E, object>>[] includes);
        Task<bool> AnyAsync(Expression<Func<E, bool>> predicate = null, Func<IQueryable<E>, IIncludableQueryable<E, object>> include = null);
        Task<E?> GetByIdAsync(bool readOnly = true, params object[]? keys);
        IQueryable<E> GetAll(bool readOnly, Expression<Func<E, bool>> func, params Expression<Func<E, object>>[] includes);
        Task<IEnumerable<D?>> GetAllDTOs(Expression<Func<E, bool>> func, params Expression<Func<E, object>>[] includes);
        Task<TResult> GetThenInclude<TResult>(Expression<Func<E, TResult>> selector, Expression<Func<E, bool>> func, Func<IQueryable<E>, IIncludableQueryable<E, object>> includes, bool disableTracking, bool readOnly);
        Task<D?> CreateAsync(D model, bool needToReturnId = false);
        Task<E?> CreateEntityAsync(E entity, bool needToReturnId = false, bool changeTraceableData = false);
        Task<D?> UpdateAsync(D model, bool needToReturnId = false, E data = null);
        Task<D?> UpdateEntityAsync(E entity, bool needToReturnId = false);
        Task<D?> DeleteAsync(K id);
        Task<D> GeneralValidation(D model);
        Task<D> CreateValidation(D model);
        Task<D> UpdateValidation(D model);
        Task<D> DeleteValidation(K id);
        E SetTraceableEntity(E entity);
    }
}
