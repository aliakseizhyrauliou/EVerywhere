using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.Balance.Domain.Entities;

public class Receipt : BaseAuditableEntity
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required string UserId { get; set; }
    
    /// <summary>
    /// Id транзакции в платежной системе
    /// </summary>
    public required string? PaymentSystemTransactionId { get; set; } 
    
    /// <summary>
    /// Id того, за что платим
    /// </summary>
    public required string PaidResourceId { get; set; } = null!;

    /// <summary>
    /// Ссылка на чек
    /// </summary>
    public string Url { get; set; } = null!;
    
    /// <summary>
    /// Инентефикатор конфигурации платежной системы
    /// </summary>
    public long? PaymentSystemConfigurationId { get; set; }
    public PaymentSystemConfiguration? PaymentSystemConfiguration { get; set; }

    
    /// <summary>
    /// Карта
    /// </summary>
    public long? PaymentMethodId { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }


    /// <summary>
    /// Платеж
    /// </summary>
    public long? PaymentId { get; set; }
    public Payment? Payment { get; set; }

    /// <summary>
    /// Холд
    /// </summary>
    public long? HoldId { get; set; }
    public Hold? Hold { get; set; }


    /// <summary>
    /// Тип платного ресурса
    /// </summary>
    public long? PaidResourceTypeId { get; set; }
    public PaidResourceType PaidResourceType { get; set; }

    public bool IsReceiptForHold { get; set; }
    public bool IsReceiptForPayment { get; set; }
}