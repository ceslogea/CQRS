using Events.Contracts;
using Hotel.Api.Application.Interfaces;
using Hotel.Api.Domain.Data;
using Hotel.Api.Domain.Entities;
using Hotel.Application.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hotel.Api.Application;

public class HotelService : IHotelService
{
    private readonly IApplicationDbContext dbContext;
    private readonly IBus massTransitBus;
    private readonly ILogger<HotelService> logger;

    public HotelService(IApplicationDbContext dbContext, IBus massTransitBus, ILogger<HotelService> logger)
    {
        this.dbContext = dbContext;
        this.massTransitBus = massTransitBus;
        this.logger = logger;
    }

    public async Task<IResult> CreateHotelAsync(CreateHotelDTO createHotelDTO, CancellationToken cancellationToken)
    {
        var item = new CreateHotelDomain(createHotelDTO);
        if (!item.IsValid()) return TypedResults.BadRequest();

        HotelEntity entity = item.CreateDataEntity();
        await dbContext.Hotels.AddAsync(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation($"entry saved {entity.Id}");

        // TODO Usar algo asincrono aqui.
        IHotelCreatedEvent hotelCreatedEvent =  item.CreateEvent();
        await massTransitBus.Publish(hotelCreatedEvent, cancellationToken);
        logger.LogInformation($"Event published saved {entity.Id}");

        return TypedResults.Created($"/hotel/{entity.Id}", createHotelDTO);
    }

    public async Task<IResult> DeleteHotel(Guid id, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Hotels.FindAsync(new object[] { id }, cancellationToken); ;
        if (entity is HotelEntity)
        {
            dbContext.Hotels.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            // TODO Usar algo asincrono aqui.
            await massTransitBus.Publish<IHotelDeletedEvent>(new { Id = id }, cancellationToken);
            logger.LogInformation($"Event published saved {id}");
        }

        return TypedResults.NoContent();
    }
}
