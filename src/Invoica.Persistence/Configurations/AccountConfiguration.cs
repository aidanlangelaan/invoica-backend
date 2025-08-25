using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoica.Persistence.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        // Configure properties
        builder.Property(a => a.Name)
            .IsRequired()
            .HasColumnType("varchar(255)");

        builder.Property(j => j.Type)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(a => a.Iban)
            .HasColumnType("varchar(34)");

        builder.Property(a => a.Description)
            .HasColumnType("varchar(255)");

        builder.Property(a => a.CurrentBalance)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(a => a.IncludedInNetWorth)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(a => a.CanTransferFrom)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(a => a.CanTransferTo)
            .IsRequired()
            .HasColumnType("boolean");

        // Indexes
        builder.HasIndex(a => new { a.CreatedById, a.Iban });

        // Configure relationships
        builder.HasOne(a => a.CreatedBy)
            .WithMany(u => u.Accounts)
            .HasForeignKey(a => a.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
