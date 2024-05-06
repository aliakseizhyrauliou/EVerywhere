using EVerywhere.ChargerPoint.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.ChargerPoint.Infrastructure.Data.Configuration;

public class ConnectorConfiguration :  IEntityTypeConfiguration<Connector>
{
    public void Configure(EntityTypeBuilder<Connector> builder)
    {
        builder.ToTable("connectors");

        builder
            .Property(x => x.Id)
            .HasColumnName("connector_id");
    }
}