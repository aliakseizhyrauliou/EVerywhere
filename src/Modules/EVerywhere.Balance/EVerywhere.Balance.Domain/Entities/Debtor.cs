using System.ComponentModel.DataAnnotations.Schema;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Entities;

public class Debtor : BaseAuditableEntity
{
    public string? UserId { get; set; }

    public required decimal Amount { get; set; }

    /// <summary>
    /// Карта
    /// </summary>
    public long PaymentMethodId { get; set; }

    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public long? PaidResourceTypeId { get; set; }

    public PaidResourceType? PaidResourceType { get; set; }

    /// <summary>
    /// Платежная система
    /// </summary>
    public long PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }
    

    public required string OperatorId { get; set; }
    public string? PaidResourceId { get; set; }

    public long? NewPaymentId { get; set; }
    public Payment? NewPayment { get; set; }

    /// <summary>
    /// Дополнительная информация
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }

    public int CaptureAttemptCount { get; set; }
    
    public DateTimeOffset? LastCaptureAttempt { get; set; }

    public string? DebtorCaptureLastErrorMessage { get; set; }

    public bool IsCaptured { get; set; }

    public bool NeedToCapture { get; set; }
}