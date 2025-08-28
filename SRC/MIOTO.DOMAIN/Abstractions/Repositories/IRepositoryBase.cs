using System.Formats.Tar;
using System.Linq.Expressions;
using MIOTO.DOMAIN.Abstractions.Entities;

namespace MIOTO.DOMAIN.Abstractions.Repositories;
public interface IRepositoryBase<TEntity, in TKey> where TEntity : class
{
    Task<TEntity?> FindByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task RemoveMultipleAsync(List<TEntity> entities);
}