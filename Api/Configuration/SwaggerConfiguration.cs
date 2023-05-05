using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Api.Configuration;

public static class SwaggerConfig
{

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(conf =>
        {
            var security = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            conf.AddSecurityDefinition(security.Reference.Id, security);
            conf.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {security, Array.Empty<string>()}
          });

            conf.SwaggerDoc("v1", new OpenApiInfo()
            {
                Title = "StandBy Test",
                Contact = new OpenApiContact() { Name = "Lucas Vinícius da Luz", Email = "daluzdev@icloud.com" }
            });

            conf.EnableAnnotations();
           

        });
        return services;
    }

    public static WebApplication UseSwaggerConf(this WebApplication app, IWebHostEnvironment env)
    {

        if (env.IsDevelopment() || env.IsStaging())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
        }

        return app;
    }

}