using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Events.Payments;

public class CreatePaymentEvent(Payment payment) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
}