// using MassTransit;
// using Hotel.Query.Data;
// using Hotel.Api.Contracts.Events;
// using Infrastructure.Data.Entities;

// namespace Hotel.Query.Consumers
// {
//     public class HotelCreatedConsumer :
//         IConsumer<IHotelCreatedEvent>
//     {

//         private readonly HotelDbContext db;
//         private readonly ILogger<HotelCreatedConsumer> logger;

//         public HotelCreatedConsumer(HotelDbContext db, ILogger<HotelCreatedConsumer> logger)
//         {
//             this.db = db;
//             this.logger = logger;
//         }

//         public async Task Consume(ConsumeContext<IHotelCreatedEvent> context)
//         {
//             logger.LogInformation($"NotificationCreated event consumed. Message: CorrelationId - {context.CorrelationId} EntityId - {context.Message.Id}");
//             await db.Hotels.AddAsync(MapToEntity(context));
//         }

//         private static HotelEntity MapToEntity(ConsumeContext<IHotelCreatedEvent> context)
//         {
//             return new HotelEntity
//             {
//                 Id = context.Message.Id,
//                 HotelName = context.Message.HotelName,
//                 Description = context.Message.Description,
//                 DescriptionFr = context.Message.DescriptionFr,
//                 Category = context.Message.Category,
//                 Tags = context.Message.Tags,
//                 ParkingIncluded = context.Message.ParkingIncluded,
//                 LastRenovationDate = context.Message.LastRenovationDate,
//                 Rating = context.Message.Rating,
//                 Address = new Address
//                 {
//                     Id = context.Message.Address.Id,
//                     StreetAddress = context.Message.Address.StreetAddress,
//                     City = context.Message.Address.City,
//                     Country = context.Message.Address.Country,
//                     PostalCode = context.Message.Address.PostalCode,
//                     StateProvince = context.Message.Address.StateProvince
//                 }
//             };
//         }
//     }
// }