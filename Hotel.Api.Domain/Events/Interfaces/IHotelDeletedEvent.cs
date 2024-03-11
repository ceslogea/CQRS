using MassTransit;

namespace Events.Contracts;

[EntityName("hotel-deleted-event-hotel-api")]
public interface IHotelDeletedEvent
{
    public Guid Id { get; set; }
}
