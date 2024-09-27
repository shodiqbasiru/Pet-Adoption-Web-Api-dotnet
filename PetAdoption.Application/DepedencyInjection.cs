using Microsoft.Extensions.DependencyInjection;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Services;

namespace PetAdoption.Application;

public static class DepedencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IStoreService, StoreService>()
                .AddScoped<IReviewService, ReviewService>()
                .AddScoped<IJwtUtils, JwtUtils>();
    }
}
