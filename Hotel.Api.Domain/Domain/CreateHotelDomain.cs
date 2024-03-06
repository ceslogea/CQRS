using Hotel.Api.Contracts;
using Hotel.Api.Contracts.Events;
using Infrastructure.Data.Entities;

namespace Hotel.Api.Domain;

public class CreateHotelDomain
{
    public CreateHotelDTO createHotelDTO;
    public CreateHotelDomain(CreateHotelDTO createHotelDTO)
    {
        this.createHotelDTO = createHotelDTO;
    }

    public bool IsValid()
    {
        return true;
    }

    public Guid HotelId => Guid.NewGuid();
    public Guid AddressId => Guid.NewGuid();

    public IHotelEntity CreateEntity()
    {
        return new HotelEntity()
        {
            Id = HotelId,
            HotelName = createHotelDTO.HotelName,
            Description = createHotelDTO.Description,
            DescriptionFr = createHotelDTO.DescriptionFr,
            Category = createHotelDTO.Category,
            Tags = createHotelDTO.Tags,
            ParkingIncluded = createHotelDTO.ParkingIncluded,
            LastRenovationDate = createHotelDTO.LastRenovationDate,
            Rating = createHotelDTO.Rating,
            Address = new Address
            {
                Id = AddressId,
                StreetAddress = createHotelDTO.Address.StreetAddress,
                City = createHotelDTO.Address.City,
                Country = createHotelDTO.Address.Country,
                PostalCode = createHotelDTO.Address.PostalCode,
                StateProvince = createHotelDTO.Address.StateProvince
            }
        };
    }

    public HotelCreatedEvent CreateEvent()
    {
        return new HotelCreatedEvent()
        {
            Id = HotelId,
            HotelName = createHotelDTO.HotelName,
            Description = createHotelDTO.Description,
            DescriptionFr = createHotelDTO.DescriptionFr,
            Category = createHotelDTO.Category,
            Tags = createHotelDTO.Tags,
            ParkingIncluded = createHotelDTO.ParkingIncluded,
            LastRenovationDate = createHotelDTO.LastRenovationDate,
            Rating = createHotelDTO.Rating,
            Address = new AddressCreatedEvent
            {
                Id = AddressId,
                StreetAddress = createHotelDTO.Address.StreetAddress,
                City = createHotelDTO.Address.City,
                Country = createHotelDTO.Address.Country,
                PostalCode = createHotelDTO.Address.PostalCode,
                StateProvince = createHotelDTO.Address.StateProvince
            }
        };
    }
}
