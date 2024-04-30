using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Services;

namespace EVerywhere.Balance.Infrastructure.External.BePaid.Services;

public class BePaidConfigurationService(IPaymentSystemConfigurationRepository repository) 
    : IPaymentSystemConfigurationService
{
    public async Task<PaymentSystemConfiguration?> GetPaymentSystemConfiguration(string paymentSystemName, 
        CancellationToken cancellationToken = default)
    {
        var paymentSystemConfiguration = await repository.GetByPaymentSystemName(paymentSystemName, cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new Exception("payment_system_not_found");
        }

        return paymentSystemConfiguration;
    }
}