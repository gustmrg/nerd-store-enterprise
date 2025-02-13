using System.Net;
using Polly.CircuitBreaker;
using Refit;

namespace NSE.WebApp.MVC.Extensions;

public class ExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (CustomHttpResponseException ex)
        {
            HandleException(context, ex.StatusCode);
        }
        catch (ValidationApiException ex)
        {
            HandleException(context, ex.StatusCode);
        }
        catch (ApiException ex)
        {
            HandleException(context, ex.StatusCode);
        }
        catch (BrokenCircuitException)
        {
            HandleCircuitBreakerException(context);
        }
    }

    private static void HandleException(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }
        
        context.Response.StatusCode = (int)statusCode;
    }

    private static void HandleCircuitBreakerException(HttpContext context)
    {
        context.Response.Redirect($"/unavailable");
    }
}