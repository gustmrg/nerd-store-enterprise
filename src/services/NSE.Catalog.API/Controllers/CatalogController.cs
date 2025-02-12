using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;
using NSE.WebAPI.Core.Identity;

namespace NSE.Catalog.API.Controllers;

[ApiController]
[Authorize]
[Route("api/catalog")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [AllowAnonymous]
    [HttpGet("products")]
    public async Task<IEnumerable<Product>> Index()
    {
        return await _productRepository.GetAll();
    }

    [ClaimsAuthorize("Catalog", "Read")]
    [HttpGet("products/{id:guid}")]
    public async Task<Product> GetProductById(Guid id)
    {
        return await _productRepository.GetById(id);
    }
    
}