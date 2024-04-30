using EVerywhere.Balance.Application.Repositories;
using EVerywhere.Balance.Domain.Entities;
using EVerywhere.Balance.Domain.Events.Holds;
using MediatR;

namespace EVerywhere.Balance.Application.Features.HoldFeatures.EventHandlers;

public class CaptureHoldEventHandler(IReceiptRepository receiptRepository) 
    : INotificationHandler<CaptureHoldEvent>
{
    public Task Handle(CaptureHoldEvent notification, CancellationToken cancellationToken)
    {
        var captureHoldReceipt = new Receipt
        {
            UserId = notification.Hold.UserId,
            PaidResourceId = notification.Hold.PaidResourceId,
            PaymentSystemTransactionId = notification.Hold.PaymentSystemTransactionId,
            Url = notification.Hold.ReceiptUrl,
            PaymentSystemConfigurationId = notification.Hold.PaymentSystemConfigurationId,
            PaymentMethodId = notification.Hold.PaymentMethodId,
            HoldId = notification.Hold.Id,
            IsReceiptForHold = true,
            PaidResourceTypeId = notification.Hold.PaidResourceTypeId
        };


        notification.Hold.Receipts?.Add(captureHoldReceipt);
        return Task.CompletedTask;
    }
}