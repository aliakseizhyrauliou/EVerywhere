using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public sealed class PaymentMethodRepository(IBalanceDbContext context)
    : BaseRepository<PaymentMethod, IBalanceDbContext>(context), IPaymentMethodRepository
{
    public async Task<PaymentMethod?> GetSelectedAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _table.SingleOrDefaultAsync(x => x.UserId == userId && x.IsSelected, cancellationToken);
    }

    public async Task UnselectAllPaymentMethodsAsync(string userId, 
        CancellationToken cancellationToken = default)
    {
        await _table.Where(x => x.UserId == userId)
            .ExecuteUpdateAsync(c => c
                .SetProperty(x => x.IsSelected, false), cancellationToken);
    }
}