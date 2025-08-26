using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class OrderLineConfiguration : IEntityTypeConfiguration<OrderLine>
{
    public void Configure(EntityTypeBuilder<OrderLine> builder)
    {
        // Configure properties
        builder.Property(a => a.Description)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.Quantity)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.UnitCode)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Configure relationships
        builder.HasOne<VatRate>(x => x.VatRate)
            .WithMany(x => x.OrderLines)
            .HasForeignKey(x => x.VatRateId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure indexes
        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint(
                "CK_OrderLine_NonNegative",
                "\"Quantity\" >= 0 AND \"UnitPrice\" >= 0");
        });
    }
}
