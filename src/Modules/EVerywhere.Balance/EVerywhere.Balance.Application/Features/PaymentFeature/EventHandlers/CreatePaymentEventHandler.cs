using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Events.Payments;
using MediatR;

namespace EVerywhere.Balance.Application.Features.PaymentFeature.EventHandlers;

public class CreatePaymentEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<CreatePaymentEvent>
{
    public Task Handle(CreatePaymentEvent notification, CancellationToken cancellationToken)
    {
            var receipt = new Receipt
        {
            UserId = notification.Payment.UserId,
            PaidResourceId = notification.Payment.PaidResourceId,
            PaymentSystemTransactionId = notification.Payment.PaymentSystemTransactionId,
            Url = notification.Payment.ReceiptUrl,
            PaymentSystemConfigurationId = notification.Payment.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Payment.PaymentMethodId,
            IsReceiptForPayment = true,
            PaidResourceTypeId = notification.Payment.PaidResourceTypeId
        };

        notification.Payment.Receipts?.Add(receipt);
        return Task.CompletedTask;
    }
}