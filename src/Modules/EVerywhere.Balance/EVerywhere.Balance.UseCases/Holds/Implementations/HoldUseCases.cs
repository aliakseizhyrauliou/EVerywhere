using EVerywhere.Balance.Application.Features.HoldFeatures.Commands;
using EVerywhere.Balance.Application.Features.PaymentMethodFeatures.Queries;
using EVerywhere.Balance.UseCases.Holds.Dtos;
using EVerywhere.Balance.UseCases.Holds.Interfaces;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.ModulesCommon.UseCase;
using MediatR;

namespace EVerywhere.Balance.UseCases.Holds.Implementations;

public class HoldUseCases(IMediator mediator, IUser currentUser) : BaseUseCases(mediator, currentUser), IHoldUseCases
{
    public async Task<CreatedEntityDto<long>> HoldWithSelectedPaymentMethod(HoldWithSelectedPaymentMethodDto dto, CancellationToken cancellationToken = default)
    {
        var selectedMethod = await mediator.Send(new GetSelectedPaymentMethodByUserIdQuery
        {
            UserId = currentUser.Id
        }, cancellationToken);

        var createdHold = await mediator.Send(new MakeHoldCommand
        {
            UserId = currentUser.Id,
            PaidResourceId = dto.PaidResourceId,
            OperatorId = dto.OperatorId,
            Amount = dto.Amount,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            PaymentMethodId = selectedMethod.Id,
            AdditionalData = dto.AdditionalData,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        return new CreatedEntityDto<long>(createdHold);
    }
}