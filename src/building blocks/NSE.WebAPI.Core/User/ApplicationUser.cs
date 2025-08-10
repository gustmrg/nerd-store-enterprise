using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSE.WebAPI.Core.User;

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