using NSE.ShoppingCart.API.Data;

namespace NSE.ShoppingCart.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ShoppingCartContext>();
    }
}