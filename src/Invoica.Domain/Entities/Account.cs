using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class Account : AuditableEntity
{
    public required string Name { get; set; }

    public required AccountType Type { get; set; }

    public string? Iban { get; set; }

    public string? Description { get; set; }

    public required decimal CurrentBalance { get; set; }

    public bool IncludedInNetWorth { get; set; } = true;

    public bool CanTransferFrom { get; set; } = true;

    public bool CanTransferTo { get; set; } = true;

    // Relationships
}
