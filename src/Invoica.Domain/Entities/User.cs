namespace Invoica.Domain.Entities;

public class User : AuditableEntity
{
    public required Guid KeycloakId { get; set; }

    public required string DisplayName { get; set; }

    public required string Email { get; set; }

    // Relationships
    public ICollection<Account> Accounts { get; set; } = new List<Account>();

    public ICollection<User> Users { get; set; } = new List<User>();
}
