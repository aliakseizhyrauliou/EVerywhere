using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class PaymentSystemWidgetGenerationConfiguration : IEntityTypeConfiguration<PaymentSystemWidget>
{
    public void Configure(EntityTypeBuilder<PaymentSystemWidget> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}