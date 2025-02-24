using NSE.Core.Extensions;
using NSE.Customers.API.Services;
using NSE.MessageBus;

namespace NSE.Customers.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnectionString("MessageBus"))
            .AddHostedService<CreateCustomerIntegrationHandler>();
    }
}