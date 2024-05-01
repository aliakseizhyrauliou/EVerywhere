using EVerywhere.Balance.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVerywhere.Balance.Infrastructure.Data.Configuration;

public class DebtorConfiguration: IEntityTypeConfiguration<Debtor>
{
    public void Configure(EntityTypeBuilder<Debtor> builder)
    {
        builder.ToTable("debtors");

        builder
            .Property(x => x.Id)
            .HasColumnName("debtor_id");
        
        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}