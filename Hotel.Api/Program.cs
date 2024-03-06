using CustomInitializer.Telemetry;
using Hotel.Api.Contracts;
using Hotel.Api.Data;
using Hotel.Api.Domain;
using Hotel.Api.Services;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.UseMassTransit(typeof(Program).Assembly);
builder.Services.AddDbContext<HotelContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();
builder.AddSwagger();

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<IHotelService, HotelService>();

var app = builder.Build();

RouteGroupBuilder hotel = app.MapGroup("/hotel").WithOpenApi();

hotel.MapPost("/", async (IHotelService service, [FromBody] CreateHotelDTO createHotelDTO) => await service.CreateHotelAsync(createHotelDTO));
hotel.MapDelete("/{id}", async (IHotelService service, Guid id) => await service.DeleteHotel(id))
   .Produces(StatusCodes.Status204NoContent);

app.AddSwagger();
app.UseHttpsRedirection();
app.Run();