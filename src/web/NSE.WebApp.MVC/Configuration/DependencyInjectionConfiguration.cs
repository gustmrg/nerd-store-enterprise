using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<AuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<AuthorizationDelegatingHandler>();
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IApplicationUser, ApplicationUser>();
    }
}