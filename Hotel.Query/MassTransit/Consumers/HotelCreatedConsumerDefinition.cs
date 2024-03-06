// namespace Hotel.Query.Consumers
// {
//     using MassTransit;

//     public class HotelCreatedConsumerDefinition :
//         ConsumerDefinition<HotelCreatedConsumer>
//     {
//         protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
//                                                   IConsumerConfigurator<HotelCreatedConsumer> consumerConfigurator,
//                                                   IRegistrationContext context)
//         {
//             endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
//         }
//     }
// }