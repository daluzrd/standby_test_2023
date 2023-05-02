using Infrastructure.Identity.Models.SecuritySettings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Configuration
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtAppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<JwtAppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings!.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerConfig =>
            {
                bearerConfig.RequireHttpsMetadata = false;
                bearerConfig.SaveToken = true;
                bearerConfig.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static IApplicationBuilder UseJwtAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
