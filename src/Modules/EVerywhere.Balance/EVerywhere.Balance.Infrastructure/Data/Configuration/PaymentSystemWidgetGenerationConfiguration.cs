using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemWidgetGenerationConfiguration : IEntityTypeConfiguration<PaymentSystemWidget>
{
    public void Configure(EntityTypeBuilder<PaymentSystemWidget> builder)
    {
        builder.ToTable("payment_system_widget_generations");

        builder
            .Property(x => x.Id)
            .HasColumnName("payment_system_widget_generation_id");
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}