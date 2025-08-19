using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MIOTO.DOMAIN.Abstractions.Entities;
using MIOTO.DOMAIN.Abstractions.Repositories;

namespace MIOTO.PERSISTENCE.Repositories;

public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
    where TEntity : DomainEntity<TKey>
{
    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
        => _context = context;

    public void Dispose() => _context?.Dispose();

    public TEntity FindById(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
        => FindAll(null, includeProperties).SingleOrDefault(x => x.Equals(id));


    public TEntity FindSingle(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        => FindAll(null, includeProperties).SingleOrDefault(predicate);

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        IQueryable<TEntity> items = _context.Set<TEntity>();
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                items = items.Include(includeProperty);

        if (predicate is not null)
            items.Where(predicate);

        return items;
    }

    public void Add(TEntity entity)
        => _context.Add(entity);


    public void Update(TEntity entity)
        => _context.Set<TEntity>().Update(entity);

    public void Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);


    public void RemoveMultiple(List<TEntity> entities)
        => _context.Set<TEntity>().RemoveRange(entities);


}