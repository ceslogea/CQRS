using CustomInitializer.Telemetry;
using Events.Contracts;
using Hotel.Query.AzureSearch.Services;
using Hotel.Query.Consumers;
using Hotel.Query.Data;
using Hotel.Query.OData;
using Infrastructure.Extensions;
using MassTransit;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.EntityFrameworkCore;
using OData.Swagger.Services;

var builder = WebApplication.CreateBuilder(args);

AddMasstransit(builder);

HotelsODataModel.BuildODataModel(builder);
// OData required
builder.Services.AddEndpointsApiExplorer().AddControllers();

builder.AddSwagger();
builder.Services.AddOdataSwaggerSupport();

builder.Services.AddDbContext<HotelDbContext>(opt => opt.UseInMemoryDatabase("InMemoryDatabase"));

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddSingleton<ITelemetryInitializer, MyTelemetryInitializer>();

builder.Services.AddTransient<IHotelQueryService, HotelQueryService>();
builder.Services.AddScoped<IHotelSearchIndexUpdateService, HotelSearchIndexUpdateService>();
builder.Services.AddScoped<IHotelAzSearchService, HotelAzSearchService>();


var app = builder.Build();
app.AddSwagger();

// RouteGroupBuilder hotelItems = app.MapGroup("/hotel")
//     .WithTags("hotel")
//     .WithOpenApi();
// hotelItems.MapGet("/", async (IHotelQueryService service) => await service.GetAllHotels());

// RouteGroupBuilder hotelAzSearchItems = app.MapGroup("/hotel/azure-seach")
//     .WithTags("hotel-azure-seach")
//     .WithOpenApi();
// hotelAzSearchItems.MapGet("/", async (IHotelAzSearchService _searchService) => await _searchService.SearchAsync(string.Empty));
// hotelAzSearchItems.MapGet("/{search}", async (IHotelAzSearchService _searchService, string search) => await _searchService.SearchAsync(search));
// hotelAzSearchItems.MapPost("/update-search", async (IHotelSearchIndexUpdateService searchIndexUpdater) => await searchIndexUpdater.UpdateIndexAsync());

app.UseHttpsRedirection();
// OData required
app.UseRouting().UseEndpoints(endpoints => endpoints.MapControllers());
await app.RunAsync();

static void AddMasstransit(WebApplicationBuilder builder)
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