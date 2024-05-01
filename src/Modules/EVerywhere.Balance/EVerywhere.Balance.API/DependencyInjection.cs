using EVerywhere.Balance.Infrastructure;
using EVerywhere.Balance.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.Balance.API;

public static class DependencyInjection
{
    public static IServiceCollection AddBalanceModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        services.AddBalanceInfrastructureServices(configuration)
            .AddBalanceUseCases();

        return services;
    }
}