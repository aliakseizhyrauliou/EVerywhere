using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Events.Payments;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentFeature.EventHandlers;

public class RefundPaymentEventHandler : INotificationHandler<RefundPaymentEvent>
{
    public Task Handle(RefundPaymentEvent notification, CancellationToken cancellationToken)
    {
        var receipt = new Receipt
        {
            UserId = notification.Payment.UserId,
            PaidResourceId = notification.Payment.PaidResourceId,
            PaymentSystemTransactionId = notification.Payment.PaymentSystemTransactionId,
            Url = notification.RefundReceiptUrl,
            PaymentSystemConfigurationId = notification.Payment.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Payment.PaymentMethodId,
            IsReceiptForPayment = true,
            PaidResourceTypeId = notification.Payment.PaidResourceTypeId
        };

        notification.Payment.Receipts?.Add(receipt);
        return Task.CompletedTask;
    }
}