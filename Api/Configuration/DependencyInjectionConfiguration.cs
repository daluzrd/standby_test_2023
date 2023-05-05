using Infrastructure.Data.Repositories;
using Infrastructure.Identity.Service;
using SharedKernel.Interfaces;

namespace Api.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void RegisterDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));          
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        RegisterServices(services);
    }
    
    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IAuthService, TokenService>();
    }
}