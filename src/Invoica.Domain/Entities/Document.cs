namespace Invoica.Domain.Entities;

public class Document : AuditableEntity
{
    public required int InvoiceId { get; set; }

    public required string FileName { get; set; }

    public required string ContentType { get; set; }

    public required string StoragePath { get; set; }

    // Foreign keys
    public Invoice Invoice { get; set; } = null!;
}
