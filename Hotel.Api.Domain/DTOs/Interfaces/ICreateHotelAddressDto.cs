namespace Hotel.Api.Domain.DTOs;

public interface ICreateHotelAddressDto
{
    Guid Id { get; }
    
    string StreetAddress { get; set; }

    string City { get; set; }

    string StateProvince { get; set; }

    string PostalCode { get; set; }

    string Country { get; set; }
}
