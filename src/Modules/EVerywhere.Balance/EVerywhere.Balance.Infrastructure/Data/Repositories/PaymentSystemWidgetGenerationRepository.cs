using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public class PaymentSystemWidgetGenerationRepository(IBalanceDbContext context) : BaseRepository<PaymentSystemWidget, IBalanceDbContext>(context), 
    IPaymentSystemWidgetGenerationRepository
{
    public async Task<PaymentSystemWidget?> GetActiveAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        return await _table.SingleOrDefaultAsync(x => x.UserId == userId &&
                                                     !x.GotResponseFromPaymentSystem &&
                                                     !x.IsDisabled, cancellationToken);
    }

    public async Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        await _table
            .Where(x => x.UserId == userId && !x.GotResponseFromPaymentSystem)
            .ExecuteUpdateAsync(x 
                => x.SetProperty(paymentSystemWidgetGeneration => paymentSystemWidgetGeneration.IsDisabled, true), cancellationToken);
    }
}