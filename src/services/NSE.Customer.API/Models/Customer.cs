using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public class Customer : Entity, IAggregateRoot
{
    protected Customer() { }
    
    public Customer(Guid id, string name, string email, string documentNumber)
    {
        Id = id;
        Name = name;
        Email = new Email(email);
        DocumentNumber = new Cpf(documentNumber);
    }
    
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public Cpf DocumentNumber { get; private set; }
    public Address Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public void ChangeEmail(string email)
    {
        Email = new Email(email);
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }
}