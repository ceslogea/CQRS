using Bogus;
using Hotel.Api.Domain.DTOs;

namespace Hotel.Application.Contracts;

public class CreateHotelAddressDto : ICreateHotelAddressDto
{
    public Guid Id {get;} = Guid.NewGuid();
    public required string StreetAddress { get; set; }

    public required string City { get; set; }

    public required string StateProvince { get; set; }

    public required string PostalCode { get; set; }

    public required string Country { get; set; }

    public static CreateHotelAddressDto GenerateFakeAddress()
    {
        var addressFaker = new Faker<CreateHotelAddressDto>()
            .RuleFor(h => h.Id, f => Guid.NewGuid())
            .RuleFor(a => a.StreetAddress, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.StateProvince, f => f.Address.State())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, f => f.Address.Country());

        return addressFaker.Generate();
    }
}