using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class HoldConfiguration: IEntityTypeConfiguration<Hold>
{
    public void Configure(EntityTypeBuilder<Hold> builder)
    {
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasMany(x => x.Receipts)
            .WithOne(x => x.Hold)
            .HasForeignKey(x => x.HoldId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.HoldCaptureCreatedPayment)
            .WithOne(x => x.CapturedHold)
            .HasForeignKey<Payment>(x => x.CapturedHoldId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.PaymentSystemWidget)
            .WithOne(x => x.Hold)
            .HasForeignKey<PaymentSystemWidget>(x => x.HoldId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}