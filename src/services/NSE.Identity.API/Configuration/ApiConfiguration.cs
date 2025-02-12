using NSE.WebAPI.Core.Identity;

namespace NSE.Identity.API.Configuration;

public static class ApiConfiguration
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
    }
    
    public static void UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        
        app.UseAuthConfiguration();
        
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}