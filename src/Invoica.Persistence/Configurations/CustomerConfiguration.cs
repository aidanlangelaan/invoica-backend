using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        // Configure properties
        builder.Property(a => a.DisplayName)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.ContactName)
            .HasColumnType("varchar(200)");

        builder.Property(a => a.Email)
            .IsRequired()
            .HasColumnType("varchar(254)");

        builder.Property(a => a.Phone)
            .IsRequired()
            .HasColumnType("varchar(32)");

        builder.Property(a => a.VatNumber)
            .HasColumnType("varchar(32)");

        builder.Property(a => a.PaymentTermDays)
            .HasColumnType("int");

        builder.Property(a => a.DayRate)
            .HasColumnType("decimal(18,2)");

        // Configure indexes
        builder.HasIndex(x => x.DisplayName);
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.VatNumber);

        // Configure relationships
        builder.HasOne(c => c.BillingAddress)
            .WithOne()
            .HasForeignKey<Customer>(c => c.BillingAddressId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
