using System.Reflection;
using EVerywhere.ModulesCommon.Infrastructure.Interceptors;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.ModulesCommon;

public static class DependencyInjection
{
    public static IServiceCollection AddModulesCommon(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCommonModuleInfrastructure();

        return serviceCollection;
    }

    /// <summary>
    /// Все модули будут использовать Interceptors
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    private static IServiceCollection AddCommonModuleInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddInterceptors()
            .AddSingleton(TimeProvider.System);
    }
    

    private static IServiceCollection AddInterceptors(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>()
            .AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
    }
}