using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        // Configure properties
        builder.Property(a => a.FileName)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(a => a.ContentType)
            .IsRequired()
            .HasColumnType("varchar(128)");

        builder.Property(a => a.StoragePath)
            .IsRequired()
            .HasColumnType("varchar(255)");

        // Configure relationships
        builder.HasOne(d => d.Invoice)
            .WithMany(i => i.Documents)
            .HasForeignKey(d => d.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
