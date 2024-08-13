using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Infrastructure.Config;
using PetAdoption.Infrastructure.Interfaces;
using PetAdoption.Infrastructure.Repositories;

namespace PetAdoption.Infrastructure;

public static class DepedencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(opt => 
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), opt => opt.MigrationsAssembly("PetAdoption.API"));
        });

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
