using System.Security.Claims;

namespace NSE.WebApp.MVC.Extensions;

public interface IApplicationUser
{
    string Name { get; }
    Guid GetUserId();
    string GetUserEmail();
    string GetUserToken();
    bool IsAuthenticated();
    bool HasRole(string role);
    IEnumerable<Claim> GetClaims();
    HttpContext GetHttpContext();
}

public class ApplicationUser : IApplicationUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApplicationUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Name => _httpContextAccessor.HttpContext.User.Identity.Name;
    
    public Guid GetUserId()
    {
        return IsAuthenticated() ? Guid.Parse(_httpContextAccessor.HttpContext.User.GetUserId()) : Guid.Empty;
    }

    public string GetUserEmail()
    {
        return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserEmail() : string.Empty;
    }

    public string GetUserToken()
    {
        return IsAuthenticated() ? _httpContextAccessor.HttpContext.User.GetUserToken() : string.Empty;
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
    }

    public bool HasRole(string role)
    {
        return _httpContextAccessor.HttpContext.User.IsInRole(role);
    }

    public IEnumerable<Claim> GetClaims()
    {
        return _httpContextAccessor.HttpContext.User.Claims;
    }

    public HttpContext GetHttpContext()
    {
        return _httpContextAccessor.HttpContext;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        var claim = claimsPrincipal.FindFirst("sub");
        return claim?.Value;
    }
    
    public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        var claim = claimsPrincipal.FindFirst("email");
        return claim?.Value;
    }
    
    public static string GetUserToken(this ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new ArgumentNullException(nameof(claimsPrincipal));
        }

        var claim = claimsPrincipal.FindFirst("JWT");
        return claim?.Value;
    }
}