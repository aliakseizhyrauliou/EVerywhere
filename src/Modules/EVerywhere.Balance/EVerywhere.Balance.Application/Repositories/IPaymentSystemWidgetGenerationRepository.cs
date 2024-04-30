using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Interfaces;

namespace EVerywhere.Balance.Application.Repositories;

public interface IPaymentSystemWidgetGenerationRepository : IBaseRepository<PaymentSystemWidget>
{
    Task<PaymentSystemWidget?> GetActiveAsync(string userId,
        CancellationToken cancellationToken = default);
    
    Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default);
}