using DotNetCore.CAP;
using DotNetCore.CAP.Internal;
using DotNetCore.CAP.Serialization;
using LINGYUN.Abp.EventBus.CAP;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// CAP ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds and configures the consistence services for the consistency.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="capAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddCAPEventBus(this IServiceCollection services, Action<CapOptions> capAction)
    {
        services.AddCap(capAction);
        // Replace with your own implementation
        services.AddSingleton<ISubscribeInvoker, AbpCAPSubscribeInvoker>();
        services.AddSingleton<ISerializer, AbpCapSerializer>();

        // Remove the default CAP startup interface
        services.RemoveAll(service =>
        {
            if (service.ServiceType.IsAssignableFrom(typeof(IBootstrapper)))
            {
                return true;
            }
            // Default Bootstrapper
            if (service.ImplementationType != null &&
                service.ImplementationType.IsAssignableTo(typeof(IBootstrapper)))
            {
                return true;
            }
            // Default Bootstrapper HostService
            if (service.ImplementationFactory != null &&
                service.ImplementationFactory.Method.ReturnType.IsAssignableTo(typeof(IBootstrapper)))
            {
                return true;
            }

            return false;
        });
        // Use the rewritten interface, do not use BackgroundService
        services.AddSingleton<AbpCAPBootstrapper>();
        services.AddSingleton<IBootstrapper>(sp => sp.GetRequiredService<AbpCAPBootstrapper>());

        return services;
    }
}
