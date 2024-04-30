using System.ComponentModel.DataAnnotations.Schema;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Entities;

public class Payment : BaseAuditableEntity
{
    /// <summary>
    /// Идентификатор пользователя 
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Сумма платежа
    /// </summary>
    public required decimal Amount { get; set; }
        
    /// <summary>
    ///Id того, за что была оплата
    ///Id бронирования, зарядки или парковки
    /// </summary>
    public required string PaidResourceId { get; set; }

    /// <summary>
    /// Идентификатор платежа в платежной системе
    /// </summary>
    public required string PaymentSystemTransactionId { get; set; } 

    /// <summary>
    /// Специфическая инфа платежа.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public string? AdditionalData { get; set; }
    
    
    /// <summary>
    /// Получатель суммы транзакции
    /// </summary>
    public required string OperatorId { get; set; }
        
    /// <summary>
    /// Успешная ли транзакция
    /// </summary>
    public bool IsSuccess { get; set; }
    
    /// <summary>
    /// Была ли оплата за счет бонусов
    /// </summary>
    public bool IsBonus { get; set; }
    
    /// <summary>
    /// Id платежного метода
    /// </summary>
    public long? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }

    /// <summary>
    /// Тип платного ресурса(зарядка, парковка и тд...)
    /// </summary>
    public long? PaidResourceTypeId { get; set; }
    public PaidResourceType? PaidResourceType { get; set; }


    /// <summary>
    /// Id конфигурации платежной системы
    /// </summary>
    public long PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }
    
    public string? ReceiptUrl { get; set; }
    
    public ICollection<Receipt>? Receipts { get; set; } = new List<Receipt>();

    /// <summary>
    /// Id холда который породил этот платеж
    /// </summary>
    public long? CapturedHoldId { get; set; }
    public Hold? CapturedHold { get; set; }

    /// <summary>
    /// Идентификатор платежного виджета если есть
    /// </summary>
    public long? PaymentSystemWidgetId { get; set; }
    public PaymentSystemWidget? PaymentSystemWidgets { get; set; }

    /// <summary>
    /// Если этот платеж был создан для должника
    /// </summary>
    public long? CaptureDebtorId { get; set; }
    public Debtor? CaptureDebtor { get; set; }

    
    public bool IsRefund { get; set; }
}