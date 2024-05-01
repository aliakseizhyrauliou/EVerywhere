using EVerywhere.Balance.UseCases.Holds.Implementations;
using EVerywhere.Balance.UseCases.Holds.Interfaces;
using EVerywhere.Balance.UseCases.Payments.Implementations;
using EVerywhere.Balance.UseCases.Payments.Interfaces;
using EVerywhere.Balance.UseCases.PaymentSystemWidgets.Implementations;
using EVerywhere.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EVerywhere.Balance.UseCases;

public static class DependencyInjection
{
    public static IServiceCollection AddBalanceUseCases(this IServiceCollection services)
    {
        services.AddScoped<IHoldUseCases, HoldUseCases>();
        services.AddScoped<IPaymentUseCases, PaymentUseCases>();
        services.AddScoped<IWidgetUseCases, WidgetUseCases>();

        return services;
    }
}