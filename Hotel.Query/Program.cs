using Azure.Messaging.ServiceBus;
using CustomInitializer.Telemetry;
using Hotel.Api.Contracts.Events;
using Hotel.Query.Consumers;
using Hotel.Query.Data;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

AddMasstransitWithExtension(builder);
// AddMasstransit(builder);
builder.AddSwagger();
builder.Services.AddDbContext<HotelDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));
builder.Services.AddTransient<IHotelQueryService, HotelQueryService>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();

var app = builder.Build();
app.AddSwagger();

RouteGroupBuilder hotelItems = app.MapGroup("/hotel"); ;
hotelItems.MapGet("/", async (IHotelQueryService service) => await service.GetAllHotels());
// hotelItems.MapGet("/complete", async (IHotelQueryService service) => await service.GetCompleteTodos());
// hotelItems.MapGet("/{id}", async (IHotelQueryService service, int id) => await service.GetTodo(id))
//    .Produces<Todo>(StatusCodes.Status200OK)
//    .Produces(StatusCodes.Status404NotFound);

app.UseHttpsRedirection();
app.Run();

static void AddMasstransitWithExtension(WebApplicationBuilder builder)
{
       builder.UseMassTransit(typeof(Program).Assembly, (context, cfg) =>
       {
              cfg.SubscriptionEndpoint<IHotelCreatedEvent>("hotel-query-sub", e =>
              {
                     e.LockDuration = TimeSpan.FromMinutes(5);
                     e.ConfigureConsumer<HotelCreatedQueryApiConsumer>(context);
              });
       });
}

static void AddMasstransit(WebApplicationBuilder builder)
{
       builder.Services.AddMassTransit(x =>
       {
              x.SetKebabCaseEndpointNameFormatter();

              var assembly = typeof(Program).Assembly;
              x.SetInMemorySagaRepositoryProvider();
              x.AddConsumers(assembly);
              x.AddSagaStateMachines(assembly);
              x.AddSagas(assembly);
              x.AddActivities(assembly);

              x.UsingAzureServiceBus((context, cfg) =>
           {
                  var connectionString = builder.Configuration["AzServiceBus:ConnectionString"] ?? throw new ArgumentException("AzServiceBus:ConnectionString");
                  cfg.Host(connectionString, h =>
               {
                      h.TransportType = ServiceBusTransportType.AmqpWebSockets;
               });

                  // Se não colocar essa config ele cria e usa uma Queue pra receber as mensagens
                  cfg.SubscriptionEndpoint<IHotelCreatedEvent>("hotel-query-sub", e =>
               {
                      e.LockDuration = TimeSpan.FromMinutes(5);
                      e.ConfigureConsumer<HotelCreatedQueryApiConsumer>(context);
               });

                  cfg.ConfigureEndpoints(context);
           });
       });
}