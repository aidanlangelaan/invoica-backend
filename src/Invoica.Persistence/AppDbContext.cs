using Invoica.Application.Common.Interfaces;
using Invoica.Domain.Entities;
using Invoica.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Invoica.Persistence;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    ICurrentUserService currentUserService,
    TimeProvider timeProvider)
    : DbContext(options)
{
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceLine> InvoiceLines { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VatRate> VatRates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.ConfigureCommonBaseEntities();

        modelBuilder.ConfigureUserRelationships();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = timeProvider.GetUtcNow().UtcDateTime;

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CreatedOnAt").CurrentValue = now;
                entry.Property("UpdatedOnAt").CurrentValue = now;
                entry.Property("CreatedById").CurrentValue = currentUserService.UserId;
                entry.Property("UpdatedById").CurrentValue = currentUserService.UserId;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property("UpdatedOnAt").CurrentValue = now;
                entry.Property("UpdatedById").CurrentValue = currentUserService.UserId;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
