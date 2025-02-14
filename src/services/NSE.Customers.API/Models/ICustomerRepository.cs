using NSE.Core.Data;

namespace NSE.Customers.API.Models;

public interface ICustomersRepository : IRepository<Customer>
{
    void AddCustomer(Customer customer);
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer> GetByDocumentNumber(string documentNumber);
}