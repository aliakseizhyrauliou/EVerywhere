using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;

namespace EVerywhere.Balance.Application.Repositories;

public interface IPaymentMethodRepository : IBaseRepository<PaymentMethod>
{
    Task<PaymentMethod?> GetSelectedAsync(string userId, 
        CancellationToken cancellationToken = default);
    
    Task UnselectAllPaymentMethodsAsync(string userId, 
        CancellationToken cancellationToken = default);
}