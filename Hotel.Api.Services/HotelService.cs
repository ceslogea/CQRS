using Hotel.Api.Contracts;
using Hotel.Api.Contracts.Events;
using Hotel.Api.Domain;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Hotel.Api.Services;

public class HotelService : IHotelService
{
    private readonly IHotelRepository repository;
    private readonly IBus massTransitBus;
    private readonly ILogger<HotelService> logger;

    public HotelService(IHotelRepository repository, IBus massTransitBus, ILogger<HotelService> logger)
    {
        this.repository = repository;
        this.massTransitBus = massTransitBus;
        this.logger = logger;
    }

    public async Task<IResult> CreateHotelAsync(CreateHotelDTO createHotelDTO)
    {
        var item = new CreateHotelDomain(createHotelDTO);
        if (!item.IsValid()) return TypedResults.BadRequest();

        await repository.AddAsync(item);
        await repository.SaveChangesAsync();
        logger.LogInformation($"entry saved {item.HotelId}");
        
        // TODO Usar algo asincrono aqui.
        using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await massTransitBus.Publish<IHotelCreatedEvent>(item.CreateEvent(), source.Token);
        logger.LogInformation($"Event published saved {item.HotelId}");

        return TypedResults.Created($"/hotel/{item.HotelId}", createHotelDTO);
    }

    // public async Task<IResult> UpdateHotel(int id, TodoItemDTO todoItemDTO, TodoDb db)
    // {
    //     var todo = await db.Todos.FindAsync(id);

    //     if (todo is null) return TypedResults.NotFound();

    //     todo.Name = todoItemDTO.Name;
    //     todo.IsComplete = todoItemDTO.IsComplete;

    //     await db.SaveChangesAsync();

    //     return TypedResults.NoContent();
    // }

    public async Task<IResult> DeleteHotel(Guid id)
    {
        await repository.DeleteAsync(id);

        // TODO Usar algo asincrono aqui.
        using var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));
        await massTransitBus.Publish<IHotelDeletedEvent>(new {Id = id}, source.Token);

        return TypedResults.NoContent();
    }
}
