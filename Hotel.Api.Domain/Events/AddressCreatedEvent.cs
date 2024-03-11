using Events.Contracts;

namespace Hotel.Api.Domain.Events;

public class AddressCreatedEvent : IAddressCreatedEvent
{
    public Guid Id { get; set; }

    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }
}
