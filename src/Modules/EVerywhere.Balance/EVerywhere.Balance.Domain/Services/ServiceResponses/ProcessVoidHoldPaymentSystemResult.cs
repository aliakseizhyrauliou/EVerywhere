using EVerywhere.Balance.Domain.Entities;

namespace EVerywhere.Balance.Domain.Services.ServiceResponses;

public class ProcessVoidHoldPaymentSystemResult
{
    public bool IsOk { get; set; }
    public string? PaymentSystemTransactionId { get; set; }
    public Hold? Hold { get; set; }
    public string? ErrorMessage { get; set; }
    public string? FriendlyErrorMessage { get; set; }
}