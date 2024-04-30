using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Domain.Services.ServiceResponses;

public class ProcessPaymentPaymentSystemWidgetResult
{
    public bool IsOk { get; set; }
    
    public Payment? Payment { get; set; }
    public PaymentSystemWidget PaymentSystemWidget { get; set; } = null!;
    public string? ErrorMessage { get; set; }
    public string? FriendlyErrorMessage { get; set; }
}