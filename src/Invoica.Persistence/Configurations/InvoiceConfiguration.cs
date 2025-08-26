using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        // Configure properties
        builder.Property(a => a.InvoiceNumber)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(a => a.IssueDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(a => a.DueDate)
            .IsRequired()
            .HasColumnType("timestamp with time zone");

        builder.Property(a => a.CurrencyCode)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.Subject)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.OwnsOne(x => x.BillingAddress, p =>
        {
            p.Property(a => a.AddressLine1)
                .IsRequired()
                .HasColumnType("varchar(200)");

            p.Property(a => a.AddressLine2)
                .HasColumnType("varchar(200)");

            p.Property(a => a.AddressLine3)
                .HasColumnType("varchar(200)");

            p.Property(a => a.PostalCode)
                .HasColumnType("varchar(32)");

            p.Property(a => a.Locality)
                .IsRequired()
                .HasColumnType("varchar(120)");

            p.Property(a => a.AdministrativeArea)
                .HasColumnType("varchar(120)");

            p.Property(a => a.CountryCode)
                .IsRequired()
                .HasColumnType("varchar(2)");
        });

        builder.Property(a => a.SubtotalExclVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.TotalVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.TotalInclVat)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.Status)
            .IsRequired()
            .HasConversion<string>();

        // Configure relationships
        builder.HasOne<Company>(x => x.Company)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Customer>(x => x.Customer)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<InvoiceLine>(x => x.InvoiceLines)
            .WithOne(x => x.Invoice)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure indexes
        builder.HasIndex(x => x.InvoiceNumber)
            .IsUnique()
            .HasDatabaseName("UX_Invoices_InvoiceNumber");
    }
}
