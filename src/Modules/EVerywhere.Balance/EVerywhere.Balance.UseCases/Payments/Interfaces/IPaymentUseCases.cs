using Balance.BePaid.UseCases.Payments.Dtos;
using EVerywhere.Balance.UseCases.Payments.Dtos;
using EVerywhere.ModulesCommon.UseCase;

namespace EVerywhere.Balance.UseCases.Payments.Interfaces;

public interface IPaymentUseCases : IBaseUseCases
{
    Task<PaymentResponseDto> PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default);
}