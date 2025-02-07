using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class IdentityController : Controller
{
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
        
        if (false) return View(model);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        return RedirectToAction("Index", "Home");
    }
}