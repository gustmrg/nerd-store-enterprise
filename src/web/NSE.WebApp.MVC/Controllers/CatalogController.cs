using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Controllers;

public class CatalogController : MainController
{
    private readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _catalogService.GetAll();
        
        return View(products);
    }

    [HttpGet]
    [Route("product-details/{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var product = await _catalogService.GetById(id);
        
        return View(product);
    }
}