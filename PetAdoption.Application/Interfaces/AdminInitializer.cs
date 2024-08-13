using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetAdoption.Core.Constants;
using PetAdoption.Core.Entities;
using PetAdoption.Infrastructure.Interfaces;

namespace PetAdoption.Application.Interfaces;

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
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        var admin = await uow.Repository<Account>().FindAsync(admin => admin.Role == Role.Admin);
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
            await uow.Repository<Account>().SaveAsync(adminAccount);
            await uow.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}