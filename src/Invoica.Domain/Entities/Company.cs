namespace Invoica.Domain.Entities;

public class Company : AuditableEntity
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required int AddressId { get; set; }

    public required string VatNumber { get; set; }

    public required string ChamberOfCommerceNumber { get; set; }

    public required string Iban { get; set; }

    public required string Bic { get; set; }

    // Foreign keys
    public Address Address { get; set; } = null!;

    // Relationships
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
