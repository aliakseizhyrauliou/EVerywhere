using Balance.BePaid.UseCases.PaymentSystemWidgets.Dtos;
using EVerywhere.Balance.Application.Features.PaymentSystemWidgetFeatures.Queries;
using EVerywhere.ModulesCommon.UseCase;

namespace EVerywhere.Balance.UseCases.PaymentSystemWidgets.Interfaces;

public interface IWidgetUseCases : IBaseUseCases
{
    Task<CheckoutDto> GenerateWidgetForCreatePaymentMethodAsync(GeneratePaymentMethodWidgetDto dto, 
        CancellationToken cancellationToken = default);
    Task<CheckoutDto> GenerateWidgetForPayment(GeneratePaymentWidgetDto command, 
        CancellationToken cancellationToken = default);
}