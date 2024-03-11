using System.Text.Json.Serialization;
using Bogus;
using Hotel.Api.Domain.DTOs;

namespace Hotel.Application.Contracts;

public class CreateHotelDTO : ICreateHotelDTO
{

    public Guid Id {get;} = Guid.NewGuid();
    public required string HotelName { get; set; }

    public required string Description { get; set; }

    public required string DescriptionFr { get; set; }

    public required string Category { get; set; }

    public required string[] Tags { get; set; }

    public bool? ParkingIncluded { get; set; }

    public DateTimeOffset? LastRenovationDate { get; set; }

    public double? Rating { get; set; }


    public required CreateHotelAddressDto Address { get; set; }

    [JsonIgnore]
    public ICreateHotelAddressDto HotelAddress {get {return Address;} }

    public static List<CreateHotelDTO> GenerateFakeHotels(int count)
    {
        var hotelFaker = new Faker<CreateHotelDTO>()
            .RuleFor(h => h.Id, f => Guid.NewGuid())
            .RuleFor(h => h.HotelName, f => f.Company.CompanyName())
            .RuleFor(h => h.Description, f => f.Company.CatchPhrase())
            .RuleFor(h => h.DescriptionFr, f => f.Company.CatchPhrase())
            .RuleFor(h => h.Category, f => f.Company.CompanySuffix())
            .RuleFor(h => h.Tags, f => f.Random.WordsArray(5))
            .RuleFor(h => h.ParkingIncluded, f => f.Random.Bool())
            .RuleFor(h => h.LastRenovationDate, f => f.Date.Past())
            .RuleFor(h => h.Rating, f => f.Random.Double(0, 5))
            .RuleFor(h => h.Address, f => CreateHotelAddressDto.GenerateFakeAddress());

        return hotelFaker.Generate(count);
    }
}
