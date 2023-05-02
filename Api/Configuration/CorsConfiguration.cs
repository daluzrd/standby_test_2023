using Api.Configuration.Security;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Api.Configuration
{
    public static class CorsConfiguration
    {
        private static bool _useCors = false;
        private static string _policy = "";

        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfig = configuration
                .GetSection(nameof(CorsConfig))
                .Get(typeof(CorsConfig)) as CorsConfig;

            if (corsConfig == null || !corsConfig.Policies.Any())
                return services;

            _useCors = true;
            _policy = corsConfig.Policies[0].Name;

            services.AddCors(options =>
            {
                foreach (var policy in corsConfig.Policies)
                {
                    options.AddPolicy(policy.Name, builder => builder.SetPolicy(policy));
                }
            });

            return services;
        }

        private static void SetPolicy(this CorsPolicyBuilder builder, CorsPolicyConfig policy)
        {
            if (policy.AllowedOrigins.Length == 1 && policy.AllowedOrigins[0] == "*")
            {
                builder.AllowAnyOrigin();
            }

            else if (policy.AllowedOrigins.Any())
            {
                builder.WithOrigins(policy.AllowedOrigins);
            }

            if (policy.AllowedMethods.Any())
            {
                builder.WithMethods(policy.AllowedMethods);
            }

            else
            {
                builder.AllowAnyMethod();
            }

            if (policy.AllowedHeaders.Any())
            {
                builder.WithHeaders(policy.AllowedHeaders);
            }

            else
            {
                builder.AllowAnyHeader();
            }

            if (policy.AllowWildcardSubdomains)
            {
                builder.SetIsOriginAllowedToAllowWildcardSubdomains();
            }
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app)
        {
            if (_useCors)
                app.UseCors(_policy);

            return app;
        }
    }
}