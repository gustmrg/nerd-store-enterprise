using System.Net.Http.Headers;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Services.Handlers;

public class AuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IApplicationUser _user;
    private readonly ILogger<AuthorizationDelegatingHandler> _logger;

    public AuthorizationDelegatingHandler(IApplicationUser user, ILogger<AuthorizationDelegatingHandler> logger)
    {
        _user = user;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.GetHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string?>() { authorizationHeader });
        }
        
        var token = _user.GetUserToken();

        if (token != null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        _logger.LogInformation("{@request}", request);
        
        return base.SendAsync(request, cancellationToken);
    }
}