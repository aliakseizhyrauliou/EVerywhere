using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemConfigurationConfiguration : IEntityTypeConfiguration<PaymentSystemConfiguration>
{
    public void Configure(EntityTypeBuilder<PaymentSystemConfiguration> builder)
    {
        builder.ToTable("payment_system_configurations");

        builder
            .Property(x => x.Id)
            .HasColumnName("payment_system_configuration_id");
        
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasMany(x => x.PaymentSystemWidgets)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Debtors)
            .WithOne(x => x.PaymentSystemConfiguration)
            .HasForeignKey(x => x.PaymentSystemConfigurationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}