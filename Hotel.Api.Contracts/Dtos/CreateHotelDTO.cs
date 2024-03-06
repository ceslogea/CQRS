namespace Hotel.Api.Contracts;

public class CreateHotelDTO
{
    public required string HotelName { get; set; }

    public required  string Description { get; set; }

    public required string DescriptionFr { get; set; }

    public required string Category { get; set; }

    public required string[] Tags { get; set; }

    public bool? ParkingIncluded { get; set; }

    public DateTimeOffset? LastRenovationDate { get; set; }

    public double? Rating { get; set; }

    public required CreateHotelAddressDto Address { get; set; }
}

public class CreateHotelAddressDto
{
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }
}