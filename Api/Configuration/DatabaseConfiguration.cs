using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Configuration
{
    public static class DatabaseConfiguration
    {
        public static IServiceCollection AddDataBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("AppConnection")));

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AppConnection"));

            services.AddScoped<DapperContext>(_ => 
            {
                return new DapperContext(configuration.GetConnectionString("AppConnection")!);
            });

            return services;
        }
    }
}