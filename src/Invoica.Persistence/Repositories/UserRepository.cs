using Invoica.Application.Common.Interfaces.Persistence;
using Invoica.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoica.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == id, ct);
    }

    public async Task<User?> GetByKeycloakIdAsync(Guid keycloakId, CancellationToken ct = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.KeycloakId == keycloakId, ct);
    }

    public async Task AddUserAsync(User user, CancellationToken ct = default)
    {
        await context.Users.AddAsync(user, ct);
    }

    public Task UpdateUserAsync(User user, CancellationToken ct = default)
    {
        context.Users.Update(user);
        return Task.CompletedTask;
    }
}
