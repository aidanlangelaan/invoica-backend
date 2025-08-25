using Invoica.Application.Common.Interfaces.Persistence;
using Invoica.Persistence.Repositories;

namespace Invoica.Persistence.Services;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IAccountRepository Accounts { get; } = new AccountRepository(context);

    public IUserRepository Users { get; } = new UserRepository(context);

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}
