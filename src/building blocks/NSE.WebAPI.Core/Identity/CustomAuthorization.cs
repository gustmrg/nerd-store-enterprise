using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NSE.WebAPI.Core.Identity;

public class CustomAuthorization
{
    public static bool ValidateUserClaims(HttpContext context, string claimType, string claimValue)
    {
        return context.User.Identity!.IsAuthenticated && context.User.Claims.Any(claim => claim.Type == claimType && claim.Value == claimValue);
    }
}

public class ClaimsAuthorizeAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizeAttribute(string claimType, string claimValue) : base(typeof(ClaimRequirementFilter))
    {
        Arguments = new object[] { new Claim(claimType, claimValue) };
    }
}

public class ClaimRequirementFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public ClaimRequirementFilter(Claim claim)
    {
        _claim = claim;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity is { IsAuthenticated: false })
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidateUserClaims(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}