using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class InvoiceLine : AuditableEntity
{
    public required int InvoiceId { get; set; }

    public required string Description { get; set; }

    public required decimal Quantity { get; set; }

    public required UnitCode UnitCode { get; set; }

    public required decimal UnitPrice { get; set; }

    public required decimal VatRate { get; set; }

    public required string VatCategoryCode { get; set; }

    public required string VatExemptionReasonCode { get; set; }

    public required string VatExemptionReason { get; set; }

    public required decimal SubtotalExclVat { get; set; }

    public required decimal TotalVat { get; set; }

    public required decimal TotalInclVat { get; set; }

    // Foreign keys
    public Invoice Invoice { get; set; } = null!;
}
