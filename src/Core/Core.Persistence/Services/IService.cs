using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Persistence.Services;
public interface IService<TEntity>
{
    Task<TEntity?> GetAsync(
    Expression<Func<TEntity, bool>> predicate,
    Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
    bool withDeleted = false,
    bool enableTracking = true,
    CancellationToken cancellationToken = default);

    Task<IPaginate<TEntity?>?> GetListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity data);
    Task<TEntity> UpdateAsync(TEntity data);
    Task<TEntity> DeleteAsync(TEntity data, bool permanent = false);
}
