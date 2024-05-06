using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.ChargerPoint.Domain.Entities;

public class SpecificOperatorChargerConfig : BaseAuditableEntity
{
    /// <summary>
    /// Идентификатор оператора
    /// </summary>
    public long OperatorId { get; set; }
    
    /// <summary>
    /// Иконка, которая будет отображаться на карте если станция активна
    /// </summary>
    public string? ChargerStatusAvailableIconUrl { get; set; }
    
    /// <summary>
    /// Иконка, которая будет отображаться на карте если станция занята
    /// </summary>
    public string? ChargerStatusOccupiedIconUrl { get; set; }
    
    /// <summary>
    /// Иконка, которая будет отображаться на карте если станция недоступна
    /// </summary>
    public string? ChargerStatusUnavailableIconUrl { get; set; }
}