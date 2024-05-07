using EVerywhere.ChargerPoint.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.ChargerPoint.API;

public static class DependencyInjection
{
    public static IServiceCollection AddChargerPointModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(DependencyInjection).Assembly);

        services.AddChargerPointInfrastructureServices(configuration);
        
        return services;
    }
}