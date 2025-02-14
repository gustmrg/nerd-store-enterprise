using Microsoft.EntityFrameworkCore;
using NSE.Customers.API.Data;
using NSE.WebAPI.Core.Identity;

namespace NSE.Customers.API.Configuration;

public static class ApiConfiguration
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CustomerContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
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
        
        app.UseAuthConfiguration();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}