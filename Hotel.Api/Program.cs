using CustomInitializer.Telemetry;
using Hotel.Api.Application;
using Hotel.Api.Application.Interfaces;
using Hotel.Api.Infrastructure.Data;
using Hotel.Application.Contracts;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.UseMassTransit(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    // options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
    options.UseInMemoryDatabase("InMemoryDatabase");
});

builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();

builder.AddSwagger();
builder.Services.AddTransient<IHotelService, HotelService>();

var app = builder.Build();

await SeedData(app);

RouteGroupBuilder hotel = app.MapGroup("/hotel").WithOpenApi();
hotel.MapPost("/", CreateTodoItem);
hotel.MapDelete("/{id}", DeleteHotel)
   .Produces(StatusCodes.Status204NoContent);

app.AddSwagger();
app.UseHttpsRedirection();
app.Run();

async Task<IResult> CreateTodoItem(IHotelService service, CreateHotelDTO createHotelDTO, CancellationToken cancellationToken)
{
    return await service.CreateHotelAsync(createHotelDTO, cancellationToken);
}

async Task<IResult> DeleteHotel(IHotelService service, Guid id, CancellationToken cancellationToken)
{
    return await service.DeleteHotel(id, cancellationToken);
}

static async Task SeedData(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var hotelService = services.GetRequiredService<IHotelService>();

        foreach (var data in CreateHotelDTO.GenerateFakeHotels(5))
            await hotelService.CreateHotelAsync(data, new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token);
    }
}