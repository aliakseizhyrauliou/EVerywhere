using EVerywhere.Balance.Domain.Events.PaymentMethods;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EVerywhere.Balance.Application.Features.PaymentMethodFeatures.EventHandlers;

public class PaymentMethodCreatedEventHandler(ILogger<PaymentMethodCreatedEventHandler> logger)
    : INotificationHandler<PaymentMethodCreatedEvent>
{
    public Task Handle(PaymentMethodCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogWarning("PaymentMethodCreatedEventHandler");
        return Task.CompletedTask;
    }
}