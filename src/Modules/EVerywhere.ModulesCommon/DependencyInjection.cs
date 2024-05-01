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

        serviceCollection.AddCommonModuleApplication();

        return serviceCollection;
    }

    private static IServiceCollection AddCommonModuleInfrastructure(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddInterceptors()
            .AddSingleton(TimeProvider.System);
    }

    private static IServiceCollection AddCommonModuleApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(Assembly.GetCallingAssembly());

        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return serviceCollection;
    }

    private static IServiceCollection AddInterceptors(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>()
            .AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
    }
}