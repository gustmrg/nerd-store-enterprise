using Microsoft.Extensions.Configuration;

namespace NSE.Core.Extensions;

public static class ConfigurationExtensions
{
    public static string GetMessageQueueConnectionString(this IConfiguration configuration, string name)
    {
        return configuration?.GetSection("MessageQueueConnection")?[name] ?? throw new InvalidOperationException();
    }
}