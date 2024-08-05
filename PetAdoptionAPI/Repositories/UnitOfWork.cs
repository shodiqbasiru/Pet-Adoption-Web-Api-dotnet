
namespace PetAdoptionAPI.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private readonly IServiceProvider _provider;

    public UnitOfWork(AppDbContext context, IServiceProvider provider)
    {
        _context = context;       
        _provider = provider;
    }

    public IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        
        return _provider.GetRequiredService<IRepository<TEntity>>();
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