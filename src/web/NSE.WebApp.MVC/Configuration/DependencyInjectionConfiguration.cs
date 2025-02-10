using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IApplicationUser, ApplicationUser>();
    }
}