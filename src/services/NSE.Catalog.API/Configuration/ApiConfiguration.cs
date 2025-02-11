using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Data;

namespace NSE.Catalog.API.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Any", builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
        
        return services;
    }

    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        
        app.UseRouting();
        
        app.UseCors("Any");
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}