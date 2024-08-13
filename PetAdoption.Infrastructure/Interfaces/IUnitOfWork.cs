namespace PetAdoption.Infrastructure.Interfaces;

public interface IUnitOfWork
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task<int> SaveChangesAsync();
    Task<TResult?> ExecuteTransactionAsync<TResult>(Func<Task<TResult?>> func);
}