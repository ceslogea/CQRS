using Hotel.Api.Application;
using Hotel.Api.Infrastructure;
using Hotel.Application.Contracts;

namespace Hotel.Api.Endpoints;

public class HotelItems : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .AllowAnonymous()
           .WithOpenApi()
           .MapPost(CreateTodoItem)
           .MapDelete(DeleteHotel, "{id}");
    }

    public async Task<IResult> CreateTodoItem(IHotelService service, CreateHotelDTO createHotelDTO, CancellationToken cancellationToken)
    {
        return await service.CreateHotelAsync(createHotelDTO, cancellationToken);
    }

    public async Task<IResult> DeleteHotel(IHotelService service, Guid id, CancellationToken cancellationToken)
    {
        return await service.DeleteHotel(id, cancellationToken);
    }
}
