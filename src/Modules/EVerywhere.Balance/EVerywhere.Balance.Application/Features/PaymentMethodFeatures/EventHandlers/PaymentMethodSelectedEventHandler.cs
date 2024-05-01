using EVerywhere.Balance.Domain.Events.PaymentMethods;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EVerywhere.Balance.Application.Features.PaymentMethodFeatures.EventHandlers;

public sealed class PaymentMethodSelectedEventHandler(ILogger<PaymentMethodCreatedEventHandler> logger)
    : INotificationHandler<PaymentMethodSelectedEvent>
{
    public Task Handle(PaymentMethodSelectedEvent notification, CancellationToken cancellationToken)
    {

        return Task.CompletedTask;
    }
}