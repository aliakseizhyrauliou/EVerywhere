using Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;
using EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Commands;
using EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Queries;
using EVerywhere.Balance.Domain.Enums;
using EVerywhere.Balance.UseCases.PaymentSystemWidgets.Interfaces;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.ModulesCommon.UseCase;
using MediatR;
using Newtonsoft.Json;

namespace EVerywhere.Balance.UseCases.PaymentSystemWidgets.Implementations;

public class WidgetUseCases(IMediator mediator, IUser currentUser) 
    : BaseUseCases(mediator, currentUser), IWidgetUseCases
{
    public async Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateWidgetCommand
        {
            WidgetReason = WidgetReason.CreatePaymentMethod,
            OperatorId = dto.OperatorId,
            AdditionalData = JsonConvert.SerializeObject(dto.AdditionalData),
            PaidResourceId = dto.PaidResourceId,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        var checkout = await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);

        return checkout;
    }

    public async Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto dto,
        CancellationToken cancellationToken = default)
    {
        await mediator.Send(new CreateWidgetCommand
        {
            WidgetReason = WidgetReason.Payment,
            OperatorId = dto.OperatorId,
            AdditionalData = JsonConvert.SerializeObject(dto.AdditionalData),
            PaidResourceId = dto.PaidResourceId,
            PaidResourceTypeId = dto.PaidResourceTypeId,
            Amount = dto.Amount,
            PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
        }, cancellationToken);

        var checkout = await mediator.Send(new GetActivePaymentSystemWidgetQuery(), cancellationToken);

        return checkout;
    }
}