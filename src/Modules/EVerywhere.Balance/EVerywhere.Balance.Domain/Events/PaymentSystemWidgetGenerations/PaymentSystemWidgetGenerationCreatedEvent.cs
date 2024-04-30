using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Events.PaymentSystemWidgetGenerations;

public class PaymentSystemWidgetGenerationCreatedEvent(PaymentSystemWidget paymentSystemWidget)
    : BaseEvent
{
    public PaymentSystemWidget PaymentSystemWidget { get; set; } = paymentSystemWidget;
}
