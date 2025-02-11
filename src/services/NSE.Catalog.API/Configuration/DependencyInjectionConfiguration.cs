using NSE.Catalog.API.Data;
using NSE.Catalog.API.Data.Repositories;
using NSE.Catalog.API.Models;

namespace NSE.Catalog.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<CatalogContext>();
    }
}