using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public class PaymentSystemWidgetGenerationRepository(IBalanceDbContext context) : BaseRepository<PaymentSystemWidget>(context), 
    IPaymentSystemWidgetGenerationRepository
{
    public async Task<PaymentSystemWidget?> GetActiveAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        return await dbSet.SingleOrDefaultAsync(x => x.UserId == userId &&
                                                     !x.GotResponseFromPaymentSystem &&
                                                     !x.IsDisabled, cancellationToken);
    }

    public async Task DisableAllUserWidgetsAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        await dbSet
            .Where(x => x.UserId == userId && !x.GotResponseFromPaymentSystem)
            .ExecuteUpdateAsync(x 
                => x.SetProperty(paymentSystemWidgetGeneration => paymentSystemWidgetGeneration.IsDisabled, true), cancellationToken);
    }
}