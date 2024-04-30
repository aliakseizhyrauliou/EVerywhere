using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasMany(x => x.Holds)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Payments)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Debtors)
            .WithOne(x => x.PaymentMethod)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Cascade);
        

    }
}