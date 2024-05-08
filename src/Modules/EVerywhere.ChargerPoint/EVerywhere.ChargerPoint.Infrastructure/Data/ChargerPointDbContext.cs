using System.Data;
using System.Reflection;
using EVerywhere.ChargerPoint.Application.Interfaces;
using EVerywhere.ChargerPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EVerywhere.ChargerPoint.Infrastructure.Data;

public class ChargerPointDbContext(DbContextOptions<ChargerPointDbContext> options) : DbContext(options), IChargerPointDbContext
{
    public async Task MigrateDatabase()
    {
        await Database.MigrateAsync();
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
        CancellationToken token = default)
    {
        return Database.BeginTransactionAsync(isolationLevel, token);
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<Charger> Chargers { get; set; }
    public DbSet<Connector> Connectors { get; set; }
    public DbSet<SpecificOperatorChargerConfig> SpecificOperatorChargerConfigs { get; set; }
}