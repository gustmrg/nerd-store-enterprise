using System.Net;

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
            HandleException(context, ex);
        }
    }

    private static void HandleException(HttpContext context, CustomHttpResponseException exception)
    {
        if (exception.StatusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }
        
        context.Response.StatusCode = (int)exception.StatusCode;
    }
}