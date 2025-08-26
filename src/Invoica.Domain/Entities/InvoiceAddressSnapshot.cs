namespace Invoica.Domain.Entities;

public sealed class InvoiceAddressSnapshot
{
    public required string AddressLine1 { get; set; }

    public string? AddressLine2 { get; set; }

    public string? AddressLine3 { get; set; }

    public string? PostalCode { get; set; }

    public required string Locality { get; set; }

    public string? AdministrativeArea { get; set; }

    public required string CountryCode { get; set; }
}
