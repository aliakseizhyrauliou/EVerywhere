using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Entities;

public class PaidResourceType : BaseAuditableEntity
{
    public required string TypeName { get; set; }

    public ICollection<Hold>? Holds { get; set; }

    public ICollection<Payment>? Payments { get; set; }

    public ICollection<PaymentSystemWidget>? PaymentSystemWidgets { get; set; }

    public ICollection<Debtor> Debtors { get; set; }

    public ICollection<Receipt> Receipts { get; set; }

}