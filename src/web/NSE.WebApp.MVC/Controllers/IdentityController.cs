using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class IdentityController : Controller
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
        
        if (false) return View(model);
        
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
        
        if (false) return View(model);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}