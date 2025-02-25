using Microsoft.EntityFrameworkCore;
using NSE.Catalog.API.Models;
using NSE.Core.Data;

namespace NSE.Catalog.API.Data.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogContext _context;

    public ProductRepository(CatalogContext catalogContext)
    {
        _context = catalogContext;
    }
    
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }

    public async Task<Product> GetById(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}