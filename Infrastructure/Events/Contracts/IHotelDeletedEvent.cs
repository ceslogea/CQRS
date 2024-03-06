using MassTransit;

namespace Hotel.Api.Contracts.Events;

[EntityName("hotel-deleted-event-hotel-api")]
public interface IHotelDeletedEvent
{
    public Guid Id { get; set; }
}
