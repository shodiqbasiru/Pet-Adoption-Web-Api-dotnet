using PetAdoptionAPI.Constants;
using PetAdoptionAPI.Entities;
using PetAdoptionAPI.Repositories;

namespace PetAdoptionAPI.Services;

public class AdminInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public AdminInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var account = scope.ServiceProvider.GetRequiredService<IRepository<Account>>();
        var persistence = scope.ServiceProvider.GetRequiredService<IPersistence>();
        var admin = await account.FindAsync(admin => admin.Role == Role.Admin);
        if (admin is null)
        {

            var hashPassword = BCrypt.Net.BCrypt.HashPassword("admin");
            
            var adminAccount = new Account
            {
                Username = "admin",
                Password = hashPassword,
                Role = Role.Admin ,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
            await account.SaveAsync(adminAccount);
            await persistence.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}