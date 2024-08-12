using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PetAdoptionAPI.Middlewares;
using PetAdoptionAPI.Repositories;
using PetAdoptionAPI.Security;
using PetAdoptionAPI.Security.impls;
using PetAdoptionAPI.Services;
using PetAdoptionAPI.Services.impls;

namespace PetAdoptionAPI.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            })
            .AddScoped(typeof(IRepository<>), typeof(Repository<>));
    }

    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<IPurchaseService, PurchaseService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ICategoryService, CategoryService>()
            .AddScoped<IServicesService, ServicesService>()
            .AddScoped<IStoreService, StoreService>()
            .AddScoped<IJwtUtils, JwtUtils>();
    }

    public static void AddMiddlewares(this IServiceCollection service)
    {
        service.AddSingleton<ExceptionHandlingMiddleware>();
    }
}