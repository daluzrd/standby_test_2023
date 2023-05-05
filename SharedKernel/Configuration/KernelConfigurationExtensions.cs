using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.MediatR;
using System.Reflection;

namespace SharedKernel.Configuration;

public static class KernelConfigurationExtensions
{
    public static IServiceCollection AddKernelConfiguration(this IServiceCollection services, Assembly assembly)
    {
        return services.AddMediatR(assembly).AddScoped<IMediatorHandler, MediatorHandler>().ConfigureMediatRHandlers(assembly);
    }

    private static IServiceCollection ConfigureMediatRHandlers(this IServiceCollection services, Assembly assembly)
    {
        var requiredTypes = new List<Type> { typeof(IRequestHandler<,>), typeof(INotificationHandler<>) };

        foreach(var type in assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract))
        {
            var ifaceTypes = type
                .GetTypeInfo()
                .ImplementedInterfaces
                .Where(i => requiredTypes.Select(x => x.Name).Contains(i.Name));
            
            foreach(var ifaceType in ifaceTypes)
            {
                services.AddTransient(ifaceType, type);
            }
        }

        return services;
    }
}