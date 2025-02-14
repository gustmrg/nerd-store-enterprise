using Microsoft.EntityFrameworkCore;
using NSE.Core.Data;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Data.Repositories;

public class CustomersRepository : ICustomersRepository
{
    private readonly CustomerContext _context;

    public CustomersRepository(CustomerContext context)
    {
        _context = context;
    }
    
    public IUnitOfWork UnitOfWork => _context;
    
    public void AddCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
    }
    

    public async Task<IEnumerable<Customer>> GetAll()
    {
        return await _context.Customers.AsNoTracking().ToListAsync();
    }

    public async Task<Customer> GetByDocumentNumber(string documentNumber)
    {
        return await _context.Customers.FirstOrDefaultAsync(c => c.DocumentNumber.Number == documentNumber);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}