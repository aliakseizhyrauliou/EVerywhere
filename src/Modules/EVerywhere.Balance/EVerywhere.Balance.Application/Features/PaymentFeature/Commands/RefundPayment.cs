using System.Data;
using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Events.Payments;
using EVerywhere.Balance.Domain.Exceptions;
using EVerywhere.Balance.Domain.Services;
using EVerywhere.ModulesCommon.Application.Exceptions;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentFeature.Commands;

public class RefundPaymentCommand : IRequest
{
    public required int PaymentId { get; set; }
}

public class RefundPaymentCommandHandler(IPaymentSystemService paymentSystemService,
    IPaymentRepository paymentRepository,
    IPaymentSystemConfigurationRepository paymentSystemConfigurationRepository) 
    : IRequestHandler<RefundPaymentCommand>
{
    public async Task Handle(RefundPaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId, cancellationToken);

        if (payment is null)
        {
            throw new NotFoundException("payment_was_not_found");
        }

        if (payment.IsRefund)
        {
            throw new InvalidArgumentException("payment_already_refunded");
        }

        var paymentSystemConfiguration = await paymentSystemConfigurationRepository.GetByIdAsync(payment.PaymentSystemConfigurationId, cancellationToken);

        if (paymentSystemConfiguration is null)
        {
            throw new Exception("payment_system_configuration_not_found");
        }

        var refundPaymentResult =
            await paymentSystemService.Refund(payment, paymentSystemConfiguration, cancellationToken);

        if (refundPaymentResult is { IsOk: false })
            throw new PaymentSystemException(refundPaymentResult.FriendlyErrorMessage);
        
        await using var transaction = await paymentRepository.BeginTransaction(IsolationLevel.ReadCommitted, cancellationToken);
        try
        {
            var refundedPayment = refundPaymentResult.Payment;

            refundedPayment.AddDomainEvent(new RefundPaymentEvent(refundedPayment, refundPaymentResult.RefundPaymentUrl));

            await paymentRepository.UpdateAsync(refundedPayment, cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}