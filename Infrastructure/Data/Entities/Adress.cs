namespace Infrastructure.Data.Entities;

public sealed class Address
{
    public Guid Id { get; set; }
    
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }
}