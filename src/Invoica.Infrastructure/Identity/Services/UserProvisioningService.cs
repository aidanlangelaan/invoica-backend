using Invoica.Application.Common.Interfaces;
using Invoica.Application.Common.Interfaces.Persistence;
using Invoica.Domain.Entities;

namespace Invoica.Infrastructure.Identity.Services;

public class UserProvisioningService(IUnitOfWork unitOfWork) : IUserProvisioningService
{
    public async Task<User> GetOrCreateUserAsync(Guid keycloakId, string? name, string? email)
    {
        var user = await unitOfWork.Users.GetByKeycloakIdAsync(keycloakId);

        if (user == null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidOperationException("Name is required for user creation.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new InvalidOperationException("Email is required for user creation.");
            }

            user = new User
            {
                KeycloakId = keycloakId,
                DisplayName = name,
                Email = email
            };

            await unitOfWork.Users.AddUserAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
        else
        {
            var updated = false;
            if (!string.IsNullOrEmpty(name) && user.DisplayName != name)
            {
                user.DisplayName = name;
                updated = true;
            }

            if (!string.IsNullOrEmpty(email) && user.Email != email)
            {
                user.Email = email;
                updated = true;
            }

            if (updated)
            {
                await unitOfWork.SaveChangesAsync();
            }
        }

        return user;
    }
}
