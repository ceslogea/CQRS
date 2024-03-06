using System.Reflection;
using Azure.Messaging.ServiceBus;
using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions;

public static class WebApplicationBuilderMassTransitExtensions
{
    public static void UseMassTransit(this WebApplicationBuilder builder,
                                      Assembly assembly,
                                      Action<IBusRegistrationContext, IServiceBusBusFactoryConfigurator>? configure = null)
    {
        builder.Services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

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

                configure?.Invoke(context, cfg);

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}