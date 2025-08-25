namespace Invoica.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    IAccountRepository Accounts { get; }

    IUserRepository Users { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
