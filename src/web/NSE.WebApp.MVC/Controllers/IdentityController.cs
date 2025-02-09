using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using IAuthenticationService = NSE.WebApp.MVC.Services.IAuthenticationService;

namespace NSE.WebApp.MVC.Controllers;

public class IdentityController : MainController
{
    private readonly IAuthenticationService _authenticationService;

    public IdentityController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    [Route("create-account")]
    public async Task<IActionResult> Register()
    {
        return View();
    }
    
    [HttpPost]
    [Route("create-account")]
    public async Task<IActionResult> Register(UserRegisterInputModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var response = await _authenticationService.RegisterAsync(model);
        
        if (ResponseHasErrors(response.ResponseResult)) return View(model);
        
        await LoginUser(response);
        
        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UserLoginInputModel model)
    {
        if (!ModelState.IsValid) return View(model);
        
        var response = await _authenticationService.LoginAsync(model);
        
        if (ResponseHasErrors(response.ResponseResult)) return View(model);

        await LoginUser(response);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    private async Task LoginUser(UserLoginResponse userLoginResponse)
    {
        var token = GetToken(userLoginResponse.AccessToken);
        
        var claims = new List<Claim>();
        claims.Add(new Claim("JWT", userLoginResponse.AccessToken));
        claims.AddRange(token.Claims);
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authenticationProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            IsPersistent = true,
        };
        
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
    }

    private static JwtSecurityToken GetToken(string jwtToken)
    {
        return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }
}