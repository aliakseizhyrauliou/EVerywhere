using AutoMapper;
using AutoMapper.QueryableExtensions;
using Balance.BePaid.Application.PaymentMethods.Queries;
using EVerywhere.Balance.Application.Interfaces;
using EVerywhere.ModulesCommon.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.Balance.Application.Features.PaymentMethodFeatures.Queries;

public class GetPaymentMethodsListQuery : IRequest<List<PaymentMethodDto>>;

public class GetPaymentMethodsListQueryHandler(IUser user, IBalanceDbContext balanceDbContext, IMapper mapper) : IRequestHandler<GetPaymentMethodsListQuery, List<PaymentMethodDto>>
{
    public async Task<List<PaymentMethodDto>> Handle(GetPaymentMethodsListQuery request, CancellationToken cancellationToken)
    {
        return await balanceDbContext.PaymentMethods
            .ProjectTo<PaymentMethodDto>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}