using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class VatRateConfiguration : IEntityTypeConfiguration<VatRate>
{
    public void Configure(EntityTypeBuilder<VatRate> builder)
    {
        // Configure properties
        builder.Property(a => a.CountryCode)
            .IsRequired()
            .HasColumnType("varchar(2)");

        builder.Property(a => a.Percentage)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(j => j.CategoryCode)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.ExemptionReasonCode)
            .HasColumnType("varchar(32)");

        builder.Property(a => a.DisplayName)
            .IsRequired()
            .HasColumnType("varchar(100)");

        builder.Property(a => a.IsActive)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(a => a.SortOrder)
            .IsRequired()
            .HasColumnType("int")
            .HasDefaultValue(0);

        // Indexes
        builder.HasIndex(x => new { x.CountryCode, x.Percentage, x.CategoryCode })
            .IsUnique()
            .HasDatabaseName("UX_VatRates_Country_Percent_Category");
        builder.HasIndex(x => x.IsActive);
        builder.HasIndex(x => x.SortOrder);

        builder.ToTable("VatRates", tb =>
        {
            // Percentage between 0 and 100
            tb.HasCheckConstraint(
                "CK_VatRate_Percentage",
                "\"Percentage\" >= 0 AND \"Percentage\" <= 100");
        });
    }
}
