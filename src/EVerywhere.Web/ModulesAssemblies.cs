using System.Reflection;
using EVerywhere.Balance.Application.Features.PaymentFeature.Commands;
using EVerywhere.ChargerPoint.Application.Features.ChargerFeatures.Commands;

namespace EVerywhere.Web;

public static class ModulesAssemblies
{
    public static Assembly[] Application =>
    [
        typeof(CreatePaymentCommand).Assembly,
        typeof(CreateChargerCommand).Assembly
    ];
}