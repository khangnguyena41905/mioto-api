using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MIOTO.DOMAIN.Abstractions.Entities;
using MIOTO.DOMAIN.Abstractions.Repositories;

namespace MIOTO.PERSISTENCE.Repositories;
public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey>, IDisposable
    where TEntity : class
{
    private readonly ApplicationDbContext _context;

    public RepositoryBase(ApplicationDbContext context)
        => _context = context;

    public void Dispose() => _context?.Dispose();

    public async Task<TEntity?> FindByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = ApplyIncludes(_context.Set<TEntity>(), includeProperties);
        return await query.FirstOrDefaultAsync(e => EF.Property<TKey>(e, "Id")!.Equals(id));
    }

    public async Task<TEntity?> FindSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = ApplyIncludes(_context.Set<TEntity>(), includeProperties);
        return await query.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>>? predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = ApplyIncludes(_context.Set<TEntity>(), includeProperties);

        if (predicate is not null)
            query = query.Where(predicate);

        return await query.ToListAsync();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);
        return Task.FromResult(entry.Entity);
    }

    public Task RemoveAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        return Task.CompletedTask;
    }

    public Task RemoveMultipleAsync(List<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
        return Task.CompletedTask;
    }

    private IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        if (includeProperties != null)
        {
            foreach (var include in includeProperties)
                query = query.Include(include);
        }

        return query;
    }
}