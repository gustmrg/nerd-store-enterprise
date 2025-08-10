using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace NSE.WebAPI.Core.User;

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