using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class MainController : Controller
{
    protected bool ResponseHasErrors(ResponseResult responseResult)
    {
        if (responseResult is not null && responseResult.Errors.Messages.Any())
        {
            foreach (var message in responseResult.Errors.Messages)
            {
                ModelState.AddModelError(string.Empty, message);    
            }
            
            return true;
        }
        
        return false;
    }
}