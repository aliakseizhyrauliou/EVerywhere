using AutoMapper;
using Balance.BePaid.Application.PaymentMethods.Queries;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.ModulesCommon.Application.Exceptions;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentMethodFeatures.Queries;

public record GetPaymentMethodByIdQuery : IRequest<PaymentMethodDto>
{
    public required long Id { get; set; }
}

public sealed class GetPaymentMethodByIdQueryHandler(IPaymentMethodRepository repository,
    IMapper mapper
    ) : IRequestHandler<GetPaymentMethodByIdQuery, PaymentMethodDto>
{
    public async Task<PaymentMethodDto> Handle(GetPaymentMethodByIdQuery request, CancellationToken cancellationToken)
    {
        var paymentMethod = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (paymentMethod is null)
        {
            throw new NotFoundException("payment_method_not_found");
        }
        
        return mapper.Map<PaymentMethodDto>(paymentMethod);
    }
}