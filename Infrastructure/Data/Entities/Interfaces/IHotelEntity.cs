namespace Infrastructure.Data.Entities;

public interface IHotelEntity
{

    Guid Id { get; set; }
    string HotelName { get; set; }
    string Description { get; set; }
    string DescriptionFr { get; set; }
    string Category { get; set; }
    string[] Tags { get; set; }
    bool? ParkingIncluded { get; set; }
    DateTimeOffset? LastRenovationDate { get; set; }
    double? Rating { get; set; }
    Address Address { get; set; }
}



