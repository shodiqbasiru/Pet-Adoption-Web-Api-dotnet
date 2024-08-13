using PetAdoption.API.Middlewares;

namespace PetAdoption.API.Extensions;

public static class DependencyInjectionExtension
{
    public static void AddMiddlewares(this IServiceCollection service)
    {
        service.AddSingleton<ExceptionHandlingMiddleware>();
    }
}