using Ardalis.GuardClauses;
using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Services;
using EVerywhere.Balance.Infrastructure.Data;
using EVerywhere.Balance.Infrastructure.Data.Repositories;
using EVerywhere.Balance.Infrastructure.External.BePaid.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.Balance.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddBalanceInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Npgsql.EVerywhere.Balance");
        
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<BalanceDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });
        
        services.AddScoped<IBalanceDbContext>(provider => provider.GetRequiredService<BalanceDbContext>());
        
        services.AddScoped<IPaymentSystemConfigurationService, BePaidConfigurationService>();
        
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddRepositories();
        services.AddServices();

        return services;
    }


    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentSystemService, BePaidService>();
        services.AddScoped<IPaymentSystemAuthorizationService, BePaidAuthorizationService>();
        services.AddScoped<IPaymentSystemConfigurationService, BePaidConfigurationService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IHoldRepository, HoldRepository>();
        services.AddScoped<IReceiptRepository, ReceiptRepository>();
        services.AddScoped<IPaidResourceTypeRepository, PaidResourceTypeRepository>();
        services.AddScoped<IPaymentSystemWidgetGenerationRepository, PaymentSystemWidgetGenerationRepository>();
        services.AddScoped<IPaymentSystemConfigurationRepository, PaymentSystemConfigurationRepository>();
        services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IDebtorRepository, DebtorRepository>();
    }
}