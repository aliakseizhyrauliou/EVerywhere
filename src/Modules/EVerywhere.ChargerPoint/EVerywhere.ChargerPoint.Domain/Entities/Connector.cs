using EVerywhere.ChargerPoint.Domain.Enums;
using EVerywhere.ModulesCommon.Domain.Models;

namespace EVerywhere.ChargerPoint.Domain.Entities;

public class Connector : BaseAuditableEntity
{
    public int Number { get; set; }
    public long ChargerId { get; set; }
    public Charger? Charger { get; set; }
    public double Power { get; set; }
    public string? SerialNum { get; set; }
    public bool Tariffed { get; set; }
    public ConnectorStatus Status { get; set; }
}