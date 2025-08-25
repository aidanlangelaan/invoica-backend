using Invoica.Domain.Entities;

namespace Invoica.Application.Common.Interfaces.Persistence;

public interface IAccountRepository
{
    Task<Account?> GetByIdAsync(int id, int? userId, CancellationToken ct);
    IQueryable<Account> GetAllAsync(int? userId);
    Task AddAsync(Account account, CancellationToken ct);
    Task UpdateAsync(Account account, CancellationToken ct);
    Task DeleteAsync(Account account, CancellationToken ct);
    Task<Account?> FindEntityByIdAsync(int id, int? userId, CancellationToken ct);
    Task<Account?> GetByNameAsync(string name, int? userId, CancellationToken ct);
    Task<Account?> GetByIbanAsync(string iban, int? userId, CancellationToken ct);
}
