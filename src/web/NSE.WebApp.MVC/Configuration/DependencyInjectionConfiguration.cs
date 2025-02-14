using Microsoft.AspNetCore.Mvc.DataAnnotations;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using Polly;
using Polly.Extensions.Http;
using Refit;

namespace NSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
        
        services.AddTransient<ExceptionHandlerMiddleware>();
        services.AddTransient<AuthorizationDelegatingHandler>();
        
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<AuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.GetRetryPolicy())
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
        
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IApplicationUser, ApplicationUser>();
    }
}