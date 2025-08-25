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
    public virtual DbSet<User> Users { get; set; }

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
