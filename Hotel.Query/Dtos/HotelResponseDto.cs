using Hotel.Query.Data;

namespace Hotel.Query.Dtos;

public class HotelResponseDto
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

    public required AddressresponseDto Address { get; set; }

    public static HotelResponseDto CreateFromQueryResult(HotelEntity hotelEntity)
    {
        return new HotelResponseDto
            {
                Id = hotelEntity.Id,
                HotelName = hotelEntity.HotelName,
                Description = hotelEntity.Description,
                DescriptionFr = hotelEntity.DescriptionFr,
                Category = hotelEntity.Category,
                Tags = hotelEntity.Tags,
                ParkingIncluded = hotelEntity.ParkingIncluded,
                LastRenovationDate = hotelEntity.LastRenovationDate,
                Rating = hotelEntity.Rating,
                Address = new AddressresponseDto
                {
                    StreetAddress = hotelEntity.Address.StreetAddress,
                    City = hotelEntity.Address.City,
                    Country = hotelEntity.Address.Country,
                    PostalCode = hotelEntity.Address.PostalCode,
                    StateProvince = hotelEntity.Address.StateProvince
                }
            };
    }
}

public class AddressresponseDto
{
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }
}
