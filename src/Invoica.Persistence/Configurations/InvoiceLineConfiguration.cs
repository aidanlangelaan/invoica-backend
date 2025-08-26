using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class InvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
{
    public void Configure(EntityTypeBuilder<InvoiceLine> builder)
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

        builder.Property(a => a.VatRate)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.VatCategoryCode)
            .IsRequired()
            .HasColumnType("varchar(3)");

        builder.Property(a => a.VatExemptionReasonCode)
            .HasColumnType("varchar(3)");

        builder.Property(a => a.VatExemptionReason)
            .HasColumnType("varchar(255)");

        builder.Property(a => a.SubtotalExclVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.TotalVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.TotalInclVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}
