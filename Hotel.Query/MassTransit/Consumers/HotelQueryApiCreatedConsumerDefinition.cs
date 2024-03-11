using MassTransit;

namespace Hotel.Query.Consumers
{

    public class HotelQueryApiCreatedConsumerDefinition :
        ConsumerDefinition<HotelCreatedQueryApiConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
                                                  IConsumerConfigurator<HotelCreatedQueryApiConsumer> consumerConfigurator,
                                                  IRegistrationContext context)
        {
            endpointConfigurator.UseMessageRetry(r => r.Intervals(500, 1000));
        }
    }
}