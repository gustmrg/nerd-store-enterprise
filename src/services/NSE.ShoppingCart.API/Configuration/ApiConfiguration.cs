using Microsoft.EntityFrameworkCore;
using NSE.ShoppingCart.API.Data;
using NSE.WebAPI.Core.Identity;

namespace NSE.ShoppingCart.API.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ShoppingCartContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddControllers();
        
        services.AddEndpointsApiExplorer();

        services.AddCors(options =>
        {
            options.AddPolicy("AnyOrigin", builder => builder
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
        
        app.UseCors("AnyOrigin");
        
        app.UseAuthConfiguration();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}