
namespace PetAdoptionAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;       
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<TResult?> ExecuteTransactionAsync<TResult>(Func<Task<TResult?>> func)
    {
        await _context.Database.BeginTransactionAsync();
        try
        {
            var result = await func();
            await _context.Database.CommitTransactionAsync();
            return result;
        }
        catch (System.Exception)
        {
            await _context.Database.RollbackTransactionAsync();
            throw;
        }
    }
}