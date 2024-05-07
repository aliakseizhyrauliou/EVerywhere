using Ardalis.GuardClauses;
using EVerywhere.ChargerPoint.Application.Interfaces;
using EVerywhere.ChargerPoint.Application.Repositories;
using EVerywhere.ChargerPoint.Infrastructure.Data;
using EVerywhere.ChargerPoint.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.ChargerPoint.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddChargerPointInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Npgsql.EVerywhere.ChargerPoint");
        
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ChargerPointDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IChargerPointDbContext>(provider => provider.GetRequiredService<ChargerPointDbContext>());

        services.AddRepositories();

        return services;
    }


    /*private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentSystemService, BePaidService>();
        services.AddScoped<IPaymentSystemAuthorizationService, BePaidAuthorizationService>();
        services.AddScoped<IPaymentSystemConfigurationService, BePaidConfigurationService>();
    }*/

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IChargerRepository, ChargerRepository>();
        services.AddScoped<IConnectorRepository, ConnectorRepository>();
    }
}