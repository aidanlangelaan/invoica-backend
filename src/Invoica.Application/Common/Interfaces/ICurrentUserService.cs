namespace Invoica.Application.Common.Interfaces;

public interface ICurrentUserService
{
    Guid? KeycloakId { get; }
    string? DisplayName { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    int? UserId { get; }

    void SetUserContext(int? userId, Guid? keycloakId, string? displayName, string? email, bool isAuthenticated = true);
}
