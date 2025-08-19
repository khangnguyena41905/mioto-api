using System.Formats.Tar;
using System.Linq.Expressions;
using MIOTO.DOMAIN.Abstractions.Entities;

namespace MIOTO.DOMAIN.Abstractions.Repositories;

public interface IRepositoryBase<TEntity, in Tkey> where TEntity : class // => in implementation should be Entity<Tkey>
{
    TEntity FindById(Tkey id, params Expression<Func<TEntity, object>>[] includeProperties);
    TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveMultiple(List<TEntity> entities);
}