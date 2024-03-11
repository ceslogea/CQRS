namespace Hotel.Api.Domain.DTOs;

public interface ICreateHotelDTO
{
    Guid Id { get; }
    
    string HotelName { get; set; }

    string Description { get; set; }

    string DescriptionFr { get; set; }

    string Category { get; set; }

    string[] Tags { get; set; }

    bool? ParkingIncluded { get; set; }

    DateTimeOffset? LastRenovationDate { get; set; }

    double? Rating { get; set; }

    ICreateHotelAddressDto HotelAddress { get; }
}
