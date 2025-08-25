using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class OrderLine : AuditableEntity
{
    public required int OrderId { get; set; }

    public required string Description { get; set; }

    public required decimal Quantity { get; set; }

    public required UnitCode UnitCode { get; set; }

    public required decimal UnitPrice { get; set; }

    public required int VatRateId { get; set; }

    // Foreign keys
    public Order Order { get; set; } = null!;

    public VatRate VatRate { get; set; } = null!;
}
