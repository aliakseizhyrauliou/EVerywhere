using EVerywhere.ChargerPoint.Domain.Enums;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.ChargerPoint.Domain.Entities;

public class Charger : BaseAuditableEntity
{
    public long OperatorId { get; set; }
    public long AggregatorId { get; set; }
    public string? SerialNumber { get; set; }
    public string? WebId { get; set; }
    public string? Address { get; set; }
    public string? OperatorSystemChargerId { get; set; }
    public ChargerStatus Status { get; set; }
    public bool IsConnected { get; set; }
    public double? Lat { get; set; }
    public double? Lon { get; set; }

    public ICollection<Connector>? Connectors { get; set; }

}