using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        // Configure properties
        builder.Property(a => a.OrderDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(a => a.Description)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(a => a.Notes)
            .HasColumnType("varchar(255)");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>();

        // Configure relationships
        builder.HasOne<Company>(o => o.Company)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Customer>(o => o.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Invoice)
            .WithOne(i => i.Order)
            .HasForeignKey<Invoice>(i => i.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.OrderLines)
            .WithOne(l => l.Order)
            .HasForeignKey(l => l.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(x => x.OrderDate);
    }
}
