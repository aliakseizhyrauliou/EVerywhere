using EVerywhere.Balance.Domain.Entities;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Events.Payments;


public class RefundPaymentEvent(Payment payment, string refundReceiptUrl) : BaseEvent
{
    public Payment Payment { get; set; } = payment;
    public string RefundReceiptUrl { get; set; } = refundReceiptUrl;
}