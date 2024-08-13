using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace PetAdoption.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity> SaveAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);
        return entry.Entity;
    }

    public TEntity Attach(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Attach(entity);
        return entry.Entity;
    }

    public async Task<TEntity?> FindByIdAsync(Guid id)
    {
        return await _context.Set<TEntity>().FindAsync(id);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(criteria);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> criteria, string[] includes)
    {
        var query = GetQueryable(includes);

        return await query.FirstOrDefaultAsync(criteria);
    }

    public async Task<List<TEntity>> FindAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria)
    {
        return await _context.Set<TEntity>().Where(criteria).ToListAsync();
    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> criteria, string[] includes)
    {
        GetQueryable(includes);
        return await _context.Set<TEntity>().Where(criteria).ToListAsync();
    }

    public Task<List<TEntity>> FindAllAsync(string[] includes)
    {
        return GetQueryable(includes).ToListAsync();
    }

    public TEntity Update(TEntity entity)
    {
        Attach(entity);
        _context.Set<TEntity>().Update(entity);
        return entity;
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    private IQueryable<TEntity> GetQueryable(string[] includes)
    {
        var query = _context.Set<TEntity>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}