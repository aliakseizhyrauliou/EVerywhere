using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;

namespace EVerywhere.Balance.Application.Repositories;

public interface IPaymentSystemConfigurationRepository : IBaseRepository<PaymentSystemConfiguration>
{
    Task<PaymentSystemConfiguration?> GetCurrentSchemaAsync(CancellationToken cancellationToken = default);
    
    Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, 
        CancellationToken cancellationToken = default);
}