using System.Reflection;
using EVerywhere.Balance.Application.Features.PaymentFeature.Commands;

namespace EVerywhere.Web;

public static class ModulesAssemblies
{
    public static Assembly[] Application =>
    [
        typeof(CreatePaymentCommand).Assembly
    ];
}