using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Api.Configuration;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Configuration;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddDataBaseConfig(builder.Configuration);

builder.Services.RegisterDI();

builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(x => 
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        x.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin);
    });

builder.Services.Configure<ApiBehaviorOptions>(options => 
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

builder.Services.AddKernelConfiguration(AppDomain.CurrentDomain.Load("Application"));

var app = builder.Build();

app.UseHttpsRedirection();

// app.UseCorsConfig();

app.UseSwaggerConf(app.Environment);

app.UseApiConfig(app.Environment);

app.UseJwtAuthentication();

app.MapControllers();

app.Run();
