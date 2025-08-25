using System.Security.Claims;
using Invoica.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Invoica.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private int? _explicitUserId;
    private Guid? _explicitKeycloakId;
    private string? _explicitDisplayName;
    private string? _explicitEmail;
    private bool? _explicitIsAuthenticated;

    public int? UserId
    {
        get
        {
            if (_explicitUserId.HasValue) return _explicitUserId.Value;

            if (httpContextAccessor.HttpContext?.Items.TryGetValue("UserId", out var value) == true && value is int id)
            {
                return id;
            }

            return null;
        }
    }

    public Guid? KeycloakId
    {
        get
        {
            if (_explicitKeycloakId.HasValue) return _explicitKeycloakId.Value;

            if (Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var keycloakId))
            {
                return keycloakId;
            }

            return null;
        }
    }

    public string? DisplayName
    {
        get
        {
            if (_explicitDisplayName != null) return _explicitDisplayName;
            return httpContextAccessor.HttpContext?.User?.FindFirst("name")
                ?.Value;
        }
    }

    public string? Email => _explicitEmail ?? httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;

    public bool IsAuthenticated
    {
        get
        {
            if (_explicitIsAuthenticated.HasValue) return _explicitIsAuthenticated.Value;
            return httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }

    public void SetUserContext(int? userId, Guid? keycloakId, string? displayName, string? email,
        bool isAuthenticated = true)
    {
        _explicitUserId = userId;
        _explicitKeycloakId = keycloakId;
        _explicitDisplayName = displayName;
        _explicitEmail = email;
        _explicitIsAuthenticated = isAuthenticated;
    }
}
