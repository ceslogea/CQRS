using Events.Contracts;
using Hotel.Api.Domain.Data;
using Hotel.Api.Domain.DTOs;
using Hotel.Api.Domain.Events;

namespace Hotel.Api.Domain.Entities;

public class CreateHotelDomain
{
    public ICreateHotelDTO createHotelDTO;
    public CreateHotelDomain(ICreateHotelDTO createHotelDTO)
    {
        this.createHotelDTO = createHotelDTO;
    }

    public bool IsValid()
    {
        return createHotelDTO is not null;
    }

    public IHotelCreatedEvent CreateEvent()
    {
        return new HotelCreatedEvent()
        {
            Id = createHotelDTO.Id,
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
                Id = createHotelDTO.HotelAddress.Id,
                StreetAddress = createHotelDTO.HotelAddress.StreetAddress,
                City = createHotelDTO.HotelAddress.City,
                Country = createHotelDTO.HotelAddress.Country,
                PostalCode = createHotelDTO.HotelAddress.PostalCode,
                StateProvince = createHotelDTO.HotelAddress.StateProvince
            }
        };
    }

    public HotelEntity CreateDataEntity()
    {
       return new HotelEntity()
        {
            Id = createHotelDTO.Id,
            HotelName = createHotelDTO.HotelName,
            Description = createHotelDTO.Description,
            DescriptionFr = createHotelDTO.DescriptionFr,
            Category = createHotelDTO.Category,
            Tags = createHotelDTO.Tags,
            ParkingIncluded = createHotelDTO.ParkingIncluded,
            LastRenovationDate = createHotelDTO.LastRenovationDate,
            Rating = createHotelDTO.Rating,
            Address = new AddressEntity
            {
                Id = createHotelDTO.HotelAddress.Id,
                StreetAddress = createHotelDTO.HotelAddress.StreetAddress,
                City = createHotelDTO.HotelAddress.City,
                Country = createHotelDTO.HotelAddress.Country,
                PostalCode = createHotelDTO.HotelAddress.PostalCode,
                StateProvince = createHotelDTO.HotelAddress.StateProvince
            }
        };
    }
}
