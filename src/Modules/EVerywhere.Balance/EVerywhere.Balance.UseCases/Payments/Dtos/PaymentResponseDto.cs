namespace EVerywhere.Balance.UseCases.Payments.Dtos;

public class PaymentResponseDto 
{
    public bool IsPaid { get; set; }
    public bool IsWrittenToDebtors { get; set; }
    public long? DebtorId { get; set; }
    public long? PaymentId { get; set; }
}