using Balance.BePaid.UseCases.Payments.Dtos;
using EVerywhere.Balance.Application.Features.DebtorFeatures.Commands;
using EVerywhere.Balance.Application.Features.PaymentFeature.Commands;
using EVerywhere.Balance.Application.Features.PaymentMethodFeatures.Queries;
using EVerywhere.Balance.Domain.Exceptions;
using EVerywhere.Balance.UseCases.Payments.Dtos;
using EVerywhere.Balance.UseCases.Payments.Interfaces;
using EVerywhere.ModulesCommon.Application.Interfaces;
using EVerywhere.ModulesCommon.UseCase;
using MediatR;

namespace EVerywhere.Balance.UseCases.Payments.Implementations;

public class PaymentUseCases(IMediator mediator, IUser currentUser) : BaseUseCases(mediator, currentUser), IPaymentUseCases
{
    public async Task<PaymentResponseDto> PaymentWithSelectedPaymentMethod(PaymentWithSelectedPaymentMethodDto dto,
        CancellationToken cancellationToken = default)
    {
        var selectedMethod = await mediator.Send(new GetSelectedPaymentMethodByUserIdQuery
        {
            UserId = currentUser.Id
        }, cancellationToken);

        try
        {
            var createdPaymentId = await mediator.Send(new CreatePaymentCommand
            {
                UserId = currentUser.Id,
                Amount = dto.Amount,
                PaidResourceId = dto.PaidResourceId,
                AdditionalData = dto.AdditionalData,
                OperatorId = dto.OperatorId,
                IsBonus = false,
                PaymentMethodId = selectedMethod.Id,
                PaidResourceTypeId = dto.PaidResourceTypeId,
                PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId
            }, cancellationToken);

            return new PaymentResponseDto
            {
                IsPaid = true,
                IsWrittenToDebtors = false,
                DebtorId = null,
                PaymentId = createdPaymentId
            };
        }
        catch (PaymentSystemException ex)
        {
            if (dto.WriteToDebtorsInCaseFail)
            {
                var createDebtorCommand = new CreateDebtorCommand
                {
                    UserId = currentUser.Id,
                    PaymentMethodId = selectedMethod.Id,
                    OperatorId = dto.OperatorId,
                    PaidResourceTypeId = dto.PaidResourceTypeId,
                    PaymentSystemConfigurationId = dto.PaymentSystemConfigurationId,
                    PaidResourceId = dto.PaidResourceId,
                    Amount = dto.Amount,
                    AdditionalData = dto.AdditionalData
                };

                var createdDebtorId = await mediator.Send(createDebtorCommand, cancellationToken);
                
                return new PaymentResponseDto
                {
                    IsPaid = false,
                    IsWrittenToDebtors = true,
                    DebtorId = createdDebtorId,
                    PaymentId = null
                };
            }
            
            return new PaymentResponseDto
            {
                IsPaid = false,
                IsWrittenToDebtors = false,
                DebtorId = null,
                PaymentId = null
            };
        }
    }
}