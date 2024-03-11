namespace Hotel.Api.Domain.Data;

public sealed class AddressEntity
{
    public Guid Id { get; set; }
    
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }
}