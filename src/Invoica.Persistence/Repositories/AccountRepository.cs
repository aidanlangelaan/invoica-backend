using Invoica.Application.Common.Interfaces.Persistence;
using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoica.Persistence.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<Account?> GetByIdAsync(int id, int? userId, CancellationToken ct)
    {
        return await context.Accounts
            .Where(a => a.Id == id && a.CreatedById == userId)
            .FirstOrDefaultAsync(ct);
    }

    public IQueryable<Account> GetAllAsync(int? userId)
    {
        return context.Accounts
            .Where(a => a.CreatedById == userId)
            .OrderBy(a => a.Name);
    }

    public async Task AddAsync(Account account, CancellationToken ct)
    {
        await context.Accounts.AddAsync(account, ct);
    }

    public Task UpdateAsync(Account account, CancellationToken ct)
    {
        context.Accounts.Update(account);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Account account, CancellationToken ct)
    {
        context.Accounts.Remove(account);
        return Task.CompletedTask;
    }

    public async Task<Account?> FindEntityByIdAsync(int id, int? userId, CancellationToken ct) =>
        await context.Accounts
            .FirstOrDefaultAsync(a => a.Id == id && a.CreatedById == userId, ct);

    public async Task<Account?> GetByNameAsync(string name, int? userId, CancellationToken ct) =>
        await context.Accounts
            .Where(a => a.Name == name && a.CreatedById == userId)
            .FirstOrDefaultAsync(ct);

    public async Task<Account?> GetByIbanAsync(string iban, int? userId, CancellationToken ct) =>
        await context.Accounts
            .Where(a => a.Iban == iban && a.CreatedById == userId)
            .FirstOrDefaultAsync(ct);
}
