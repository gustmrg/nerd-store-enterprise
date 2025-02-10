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
            errorModel.Message =
                "A página que está procurando não existe! <br />Em caso de dúvidas entre em contato com nosso suporte";
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
}