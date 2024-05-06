using EVerywhere.ChargerPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.ChargerPoint.Infrastructure.Data.Configuration;

public class ChargerConfiguration :  IEntityTypeConfiguration<Charger>
{
    public void Configure(EntityTypeBuilder<Charger> builder)
    {
        builder.ToTable("chargers");

        builder
            .Property(x => x.Id)
            .HasColumnName("charger_id");

        builder.HasMany(x => x.Connectors)
            .WithOne(x => x.Charger)
            .HasForeignKey(x => x.ChargerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.WebId);
    }
}