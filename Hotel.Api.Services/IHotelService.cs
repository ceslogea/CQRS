using Hotel.Api.Contracts;
using Microsoft.AspNetCore.Http;

namespace Hotel.Api.Services;

public interface IHotelService
{
    Task<IResult> CreateHotelAsync(CreateHotelDTO createHotelDTO);
    Task<IResult> DeleteHotel(Guid id);
}
