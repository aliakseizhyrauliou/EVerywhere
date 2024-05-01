using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Infrastructure.Data.Repositories;

public class PaymentSystemConfigurationRepository(IBalanceDbContext context)
    : BaseRepository<PaymentSystemConfiguration, IBalanceDbContext>(context), IPaymentSystemConfigurationRepository
{
    public async Task<PaymentSystemConfiguration?> GetCurrentSchemaAsync(CancellationToken cancellationToken = default)
    {
        return await _table.SingleOrDefaultAsync(x => x.IsCurrentSchema, cancellationToken);
    }

    public async Task<PaymentSystemConfiguration?> GetByPaymentSystemName(string name, CancellationToken cancellationToken = default)
    {
        return await _table.SingleOrDefaultAsync(x => x.PaymentSystemName == name, cancellationToken);
    }
}