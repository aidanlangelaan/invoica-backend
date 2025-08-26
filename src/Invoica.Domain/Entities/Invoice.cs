using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class Invoice : AuditableEntity
{
    public required string InvoiceNumber { get; set; }

    public required DateTime IssueDate { get; set; }

    public required DateTime DueDate { get; set; }

    public required CurrencyCode CurrencyCode { get; set; }

    public required int CompanyId { get; set; }

    public required int CustomerId { get; set; }

    public required int OrderId { get; set; }

    public required string Subject { get; set; }

    public InvoiceAddressSnapshot BillingAddress { get; set; } = null!;

    public required decimal SubtotalExclVat { get; set; }

    public required decimal TotalVat { get; set; }

    public required decimal TotalInclVat { get; set; }

    public required InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

    // Foreign keys
    public Company Company { get; set; } = null!;

    public Customer Customer { get; set; } = null!;

    public Order Order { get; set; } = null!;

    // Relationships
    public ICollection<InvoiceLine> InvoiceLines { get; set; } = new List<InvoiceLine>();

    public ICollection<Document> Documents { get; set; } = new List<Document>();
}
