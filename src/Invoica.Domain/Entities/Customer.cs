namespace Invoica.Domain.Entities;

public class Customer : AuditableEntity
{
    public required string DisplayName { get; set; }

    public string? ContactName { get; set; }

    public required string Email { get; set; }

    public required string Phone { get; set; }

    public required int BillingAddressId { get; set; }

    public string? VatNumber { get; set; }

    public int? PaymentTermDays { get; set; }

    public decimal? DayRate { get; set; }

    // Foreign keys
    public Address BillingAddress { get; set; } = null!;

    // Relationships
    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
