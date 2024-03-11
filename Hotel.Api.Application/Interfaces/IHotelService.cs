using Hotel.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Hotel.Api.Application;

public interface IHotelService
{
    Task<IResult> CreateHotelAsync(CreateHotelDTO createHotelDTO, CancellationToken cancellationToken);
    Task<IResult> DeleteHotel(Guid id, CancellationToken cancellationToken);
}
