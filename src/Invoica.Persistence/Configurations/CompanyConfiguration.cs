using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        // Configure properties
        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.Email)
            .IsRequired()
            .HasColumnType("varchar(254)");

        builder.Property(a => a.Phone)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder.Property(a => a.VatNumber)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder.Property(a => a.Iban)
            .HasColumnType("varchar(34)");

        builder.Property(a => a.Bic)
            .HasColumnType("varchar(11)");

        // Configure indexes
        builder.HasIndex(x => x.VatNumber);
        builder.HasIndex(x => x.ChamberOfCommerceNumber);

        // Configure relationships
        builder.HasOne(c => c.Address)
            .WithOne()
            .HasForeignKey<Company>(c => c.AddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
