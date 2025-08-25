using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class Order : AuditableEntity
{
    public required int CompanyId { get; set; }

    public required int CustomerId { get; set; }

    public required DateTime OrderDate { get; set; }

    public required string Description { get; set; }

    public string? Notes { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    // Foreign keys
    public Company Company { get; set; } = null!;

    public Customer Customer { get; set; } = null!;

    // Relationships
    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

    public Invoice? Invoice { get; set; }
}
