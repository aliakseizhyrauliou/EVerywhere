using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Domain.Services;

public interface IPaymentSystemConfigurationService
{
    
    /// <summary>
    /// Вернет кофигурацию платежной системы
    /// </summary>
    /// <param name="paymentSystemName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PaymentSystemConfiguration?> GetPaymentSystemConfiguration(string paymentSystemName,
        CancellationToken cancellationToken = default);
}