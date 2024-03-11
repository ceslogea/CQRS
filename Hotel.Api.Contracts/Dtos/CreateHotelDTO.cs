using Bogus;

namespace Hotel.Api.Contracts;

public class CreateHotelDTO
{
    public required string HotelName { get; set; }

    public required string Description { get; set; }

    public required string DescriptionFr { get; set; }

    public required string Category { get; set; }

    public required string[] Tags { get; set; }

    public bool? ParkingIncluded { get; set; }

    public DateTimeOffset? LastRenovationDate { get; set; }

    public double? Rating { get; set; }

    public required CreateHotelAddressDto Address { get; set; }

    public static List<CreateHotelDTO> GenerateFakeHotels(int count)
    {
        var hotelFaker = new Faker<CreateHotelDTO>()
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

public class CreateHotelAddressDto
{
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }

    public static CreateHotelAddressDto GenerateFakeAddress()
    {
        var addressFaker = new Faker<CreateHotelAddressDto>()
            .RuleFor(a => a.StreetAddress, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvince, f => f.Address.State())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, f => f.Address.Country());

        return addressFaker.Generate();
    }
}