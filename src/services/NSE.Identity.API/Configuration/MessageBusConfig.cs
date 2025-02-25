using NSE.Core.Extensions;
using NSE.MessageBus;

namespace NSE.Identity.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnectionString("MessageBus"));
    }
}