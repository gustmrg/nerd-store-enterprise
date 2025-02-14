using NSE.Core.Messages;

namespace NSE.Customers.API.Application.Events;

public class CustomerCreatedEvent : Event
{
    public CustomerCreatedEvent(Guid id, string name, string email, string documentNumber)
    {
        Id = id;
        Name = name;
        Email = email;
        DocumentNumber = documentNumber;
        AggregateId = id;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string DocumentNumber { get; private set; }
}