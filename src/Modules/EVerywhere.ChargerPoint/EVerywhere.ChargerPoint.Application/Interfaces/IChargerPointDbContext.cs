using EVerywhere.ChargerPoint.Domain.Entities;
using EVerywhere.ModulesCommon.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace EVerywhere.ChargerPoint.Application.Interfaces;

public interface IChargerPointDbContext : IDbContext
{
    DbSet<Charger> Chargers { get; set; }
    DbSet<Connector> Connectors { get; set; }
    DbSet<SpecificOperatorChargerConfig> SpecificOperatorChargerConfigs { get; set; }
}