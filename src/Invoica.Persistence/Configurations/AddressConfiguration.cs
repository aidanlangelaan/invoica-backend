using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        // Configure properties
        builder.Property(a => a.AddressLine1)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.AddressLine2)
            .HasColumnType("varchar(200)");

        builder.Property(a => a.AddressLine3)
            .HasColumnType("varchar(200)");

        builder.Property(a => a.PostalCode)
            .HasColumnType("varchar(32)");

        builder.Property(a => a.Locality)
            .IsRequired()
            .HasColumnType("varchar(120)");

        builder.Property(a => a.AdministrativeArea)
            .HasColumnType("varchar(120)");

        builder.Property(a => a.CountryCode)
            .IsRequired()
            .HasColumnType("varchar(2)");
    }
}
