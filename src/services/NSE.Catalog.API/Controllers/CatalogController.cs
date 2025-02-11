using Microsoft.AspNetCore.Mvc;
using NSE.Catalog.API.Models;

namespace NSE.Catalog.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("products")]
    public async Task<IEnumerable<Product>> Index()
    {
        return await _productRepository.GetAll();
    }

    [HttpGet("products/{id:guid}")]
    public async Task<Product> GetProductById(Guid id)
    {
        return await _productRepository.GetById(id);
    }
    
}