using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("erro/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var errorModel = new ErrorViewModel();

        if (id == 500)
        {
            errorModel.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
            errorModel.Title = "Ocorreu um erro!";
            errorModel.ErrorCode = id;
        }
        else if (id == 404)
        {
            errorModel.Message = "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
            errorModel.Title = "Ops! Página não encontrada.";
            errorModel.ErrorCode = id;
        }
        else if (id == 403)
        {
            errorModel.Message = "Você não tem permissão para fazer isto.";
            errorModel.Title = "Acesso Negado";
            errorModel.ErrorCode = id;
        }
        else
        {
            return StatusCode(404);
        }

        return View("Error", errorModel);
    }

    [Route("unavailable")]
    public IActionResult Unavailable()
    {
        var errorModel = new ErrorViewModel
        {
            Message =
                "O sistema está temporariamente indisponível! Isto pode ocorrer em momentos de sobrecarga de usuários. Por favor, aguarde alguns minutos.",
            Title = "Sistema indisponível",
            ErrorCode = 500
        };
        
        return View("Error", errorModel);
    }
}