using Infrastructure.Data.Repositories;
using SharedKernel.Interfaces;

namespace Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));          
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        }
    }
}