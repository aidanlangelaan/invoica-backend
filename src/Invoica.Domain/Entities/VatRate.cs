using Invoica.Domain.Enums;

namespace Invoica.Domain.Entities;

public class VatRate : AuditableEntity
{
    public required string CountryCode { get; set; }

    public required decimal Percentage { get; set; }

    public required VatCategoryCode CategoryCode { get; set; }

    public string? ExemptionReasonCode { get; set; }

    public required string DisplayName { get; set; }

    public required bool IsActive { get; set; } = true;

    public required int SortOrder { get; set; }

    // Relationships
    public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
}
