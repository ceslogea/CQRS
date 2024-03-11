using MassTransit;

namespace Events.Contracts;

[EntityName("hotel-created-event-hotel-api")]
public interface IHotelCreatedEvent
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
    IAddressCreatedEvent Address { get; set; }
}

public interface IAddressCreatedEvent
{
    public Guid Id { get; set; }

    public string StreetAddress { get; set; }

    public string City { get; set; }

    public string StateProvince { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }
}