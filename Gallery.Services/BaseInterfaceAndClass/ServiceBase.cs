using Gallery.DTO.BaseInterfaceAndClass;
using Gallery.Infrastructure.BaseInterfaceAndClass;
using Gallery.Models.BaseEntityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Gallery.Services.BaseInterfaceAndClass
{
    public class ServiceBase : IServiceBase
    {
        //public static UserContext? UserContext { get; set; } = null;
    }
    public abstract class ServiceBase<Entity, DTO, KeyTypeId> : ServiceBase, IMarker, IServiceBase<Entity, DTO, KeyTypeId> where Entity : BaseEntity<KeyTypeId> where DTO : IBaseDTO where KeyTypeId : struct
    {
        public ServiceBase(IBaseRepository<Entity> entityRepository)
        {
            RepositoryOfEntity = entityRepository;
        }

        public KeyTypeId IdOfEntity { get; private set; }
        public IBaseRepository<Entity> RepositoryOfEntity { get; private set; }

        public abstract Entity TranslateToEntity(DTO model);
        public abstract DTO TranslateToDTO(Entity entity);

        public abstract Task<DTO> GeneralValidation(DTO model);
        public abstract Task<DTO> CreateValidation(DTO model);
        public abstract Task<DTO> UpdateValidation(DTO model);
        public abstract Task<DTO> DeleteValidation(KeyTypeId id);

        public virtual async Task<Entity?> CreateEntityAsync(Entity entity, bool needToReturnId = false, bool changeTraceableData = false)
        {
            IdOfEntity = entity.Id;
            if (changeTraceableData == false)
            {
                entity.CreateDateTime = DateTime.Now.ToLocalTime();
                entity.UpdateDateTime = DateTime.Now.ToLocalTime();
                //entity.CreateUserId = UserContext != null ? UserContext.UserId : null;
                //entity.LastUpdateUserId = UserContext != null ? UserContext.UserId : null;
            }

            if (needToReturnId == false)
            {
                await RepositoryOfEntity.AddAsync(entity);
            }
            else
            {
                await CommitAsyncWithReturnId(entity, true);
            }

            return entity;
        }

        public virtual async Task<DTO> CreateAsync(DTO model, bool needToReturnId = false)
        {
            DTO fullDto = await GeneralValidation(model);
            if (await fullDto.HasError())
            {
                return model;
            }
            fullDto = await CreateValidation(model);
            if (await fullDto.HasError())
            {
                return model;
            }

            Entity entity = TranslateToEntity(fullDto);
            IdOfEntity = entity.Id;
            entity.CreateDateTime = DateTime.Now.ToLocalTime();
            entity.UpdateDateTime = DateTime.Now.ToLocalTime();
            //entity.CreateUserId = UserContext != null ? UserContext.UserId : null;
            //entity.LastUpdateUserId = UserContext != null ? UserContext.UserId : null;

            if (needToReturnId == false)
            {
                await RepositoryOfEntity.AddAsync(entity);
            }
            else
            {
                await CommitAsyncWithReturnId(entity, true);
            }

            return model;
        }

        public virtual async Task<DTO> UpdateAsync(DTO model, bool needToReturnId = false, Entity data = null)
        {
            DTO fullDto = await GeneralValidation(model);
            if (await fullDto.HasError())
            {
                return model;
            }
            fullDto = await UpdateValidation(model);
            if (await fullDto.HasError())
            {
                return model;
            }

            Entity entity = TranslateToEntity(fullDto);
            IdOfEntity = entity.Id;

            if (data == null)
            {
                entity.UpdateDateTime = DateTime.Now.ToLocalTime();
                entity.CreateDateTime = entity.CreateDateTime == DateTime.MinValue ? entity.UpdateDateTime.AddMinutes(-10).ToLocalTime() : entity.CreateDateTime;
                //entity.CreateUserId = (entity.CreateUserId == null || entity.CreateUserId == Guid.Empty) && UserContext != null ? UserContext.UserId : entity.CreateUserId;
                //entity.LastUpdateUserId = UserContext != null ? UserContext.UserId : entity.LastUpdateUserId != null && UserContext == null ? entity.LastUpdateUserId : null;
            }

            else
            {
                entity.UpdateDateTime = DateTime.Now.ToLocalTime();
                entity.CreateDateTime = data.CreateDateTime;
                entity.CreateUserId = data.CreateUserId;
                //entity.LastUpdateUserId = UserContext != null ? UserContext.UserId : entity.LastUpdateUserId != null && UserContext == null ? data.LastUpdateUserId : null;
            }

            if (needToReturnId == false)
            {
                await Task.Run(() => RepositoryOfEntity.Update(entity));
            }
            else
            {
                await CommitAsyncWithReturnId(entity, false);
            }

            return model;
        }

        public virtual async Task<DTO> UpdateEntityAsync(Entity entity, bool needToReturnId = false)
        {
            IdOfEntity = entity.Id;

            //entity.CreateDateTime = entity.CreateDateTime != DateTime.MinValue ? entity.CreateDateTime : UserContext != null && UserContext.CreateDateTime != DateTime.MinValue && entity.CreateDateTime == DateTime.MinValue ?
            //    UserContext.CreateDateTime : entity.CreateDateTime != null && UserContext == null ? entity.CreateDateTime : DateTime.Now.ToLocalTime();

            entity.UpdateDateTime = DateTime.Now.ToLocalTime();

            //entity.CreateUserId = (entity.CreateUserId == null || entity.CreateUserId == Guid.Empty) && UserContext != null ? UserContext.UserId : entity.CreateUserId;
            //entity.LastUpdateUserId = UserContext != null ? UserContext.UserId : entity.LastUpdateUserId != null && UserContext == null ? entity.LastUpdateUserId : null;

            if (needToReturnId == false)
            {
                await Task.Run(() => RepositoryOfEntity.Update(entity));
            }
            else
            {
                await CommitAsyncWithReturnId(entity, false);
            }

            return TranslateToDTO(entity);
        }

        public virtual async Task<DTO>? DeleteAsync(KeyTypeId id)
        {
            DTO fullDto = await DeleteValidation(id);
            if (await fullDto.HasError())
            {
                return fullDto;
            }
            Entity entity = TranslateToEntity(fullDto);
            RepositoryOfEntity.Delete(entity);
            return fullDto;
        }

        private async Task<KeyTypeId> CommitAsyncWithReturnId(Entity entity, bool useForAdd)
        {
            if (useForAdd)
            {
                await RepositoryOfEntity.AddAsync(entity);
            }
            else
            {
                await Task.Run(() => RepositoryOfEntity.Update(entity));
            }
            await RepositoryOfEntity.CommitAsync();
            IdOfEntity = entity.Id;
            return IdOfEntity;
        }

        public virtual Entity SetTraceableEntity(Entity entity)
        {
            entity.CreateDateTime = entity.CreateDateTime != null && entity.CreateDateTime != default ? entity.CreateDateTime : DateTime.Now.ToLocalTime();
            entity.UpdateDateTime = DateTime.Now.ToLocalTime();
            //entity.CreateUserId = entity.CreateUserId != null && entity.CreateUserId != Guid.Empty ? entity.CreateUserId : UserContext.UserId;
            //entity.LastUpdateUserId = UserContext.UserId;
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> func) => await RepositoryOfEntity.AnyAsync(func);

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> func, params Expression<Func<Entity, object>>[] includes) => await RepositoryOfEntity.AnyAsync(func, includes);

        public async Task<bool> AnyAsync(Expression<Func<Entity, bool>> predicate = null, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> include = null)
        {
            return await RepositoryOfEntity.AnyAsyncWithThenInclude(predicate, include);
        }

        public async Task<Entity> GetAsync(bool readOnly, Expression<Func<Entity, bool>> func, params Expression<Func<Entity, object>>[] includes)
        {
            Entity? findData = await RepositoryOfEntity.FirstAsync(readOnly, func, includes);
            return findData ?? default;
        }

        public async Task<TResult> GetThenInclude<TResult>(Expression<Func<Entity, TResult>> selector, Expression<Func<Entity, bool>> func, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> includes, bool disableTracking, bool readOnly)
        {
            IQueryable<TResult> getAll = RepositoryOfEntity.GetAllThenInclude(selector, func, includes, disableTracking, readOnly);
            return await Task.Run(() => getAll != null && getAll.Any() ? getAll.FirstOrDefault() : default);
        }

        public IQueryable<Entity> GetAll(bool readOnly, Expression<Func<Entity, bool>> func, params Expression<Func<Entity, object>>[] includes)
        {
            return RepositoryOfEntity.GetAll(readOnly, func, includes);
        }

        public async Task<IQueryable<TResult>> GetAllWithThenInclude<TResult>(bool readOnly, Expression<Func<Entity, bool>> func, Expression<Func<Entity, TResult>> selector, Func<IQueryable<Entity>, IIncludableQueryable<Entity, object>> includes)
        {
            return await Task.Run(() => RepositoryOfEntity.GetAllThenInclude(selector, func, includes, readOnly: readOnly));
        }

        public async Task<IEnumerable<DTO?>> GetAllDTOs(Expression<Func<Entity, bool>> func, params Expression<Func<Entity, object>>[] includes)
        {
            IQueryable<Entity> datas = GetAll(true, func, includes);
            return datas != null && await datas.AnyAsync() ? datas.Select(TranslateToDTO) : Enumerable.Empty<DTO>();
        }

        public async Task<Entity?> GetByIdAsync(bool readOnly = true, params object[]? keys) => await RepositoryOfEntity.FindByIdAsync(readOnly, keys) ?? default;

        public async Task<DTO?> GetDTO(bool readOnly, Expression<Func<Entity, bool>> func, params Expression<Func<Entity, object>>[] includes)
        {
            Entity data = await GetAsync(readOnly, func, includes);
            return data != null ? TranslateToDTO(data) : default;
        }
    }
}
