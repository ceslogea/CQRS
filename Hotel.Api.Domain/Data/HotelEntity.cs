namespace Hotel.Api.Domain.Data;

public sealed class HotelEntity 
{
    public Guid Id { get; set; }

    public required string HotelName { get; set; }

    public required string Description { get; set; }

    public required string DescriptionFr { get; set; }

    public required string Category { get; set; }

    public required string[] Tags { get; set; }

    public bool? ParkingIncluded { get; set; }

    public DateTimeOffset? LastRenovationDate { get; set; }

    public double? Rating { get; set; }

    public required AddressEntity Address { get; set; }
}

