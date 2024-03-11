using Events.Contracts;

namespace Hotel.Api.Domain.Events;

public class HotelCreatedEvent : IHotelCreatedEvent
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

    public required IAddressCreatedEvent Address { get; set; }
}
