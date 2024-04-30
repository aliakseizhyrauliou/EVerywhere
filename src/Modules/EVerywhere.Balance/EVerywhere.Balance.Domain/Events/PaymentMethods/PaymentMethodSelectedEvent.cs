using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Events.PaymentMethods;

public class PaymentMethodSelectedEvent(PaymentMethod paymentMethod) : BaseEvent
{
    public PaymentMethod PaymentMethod { get; set; } = paymentMethod;
}