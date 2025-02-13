using NSE.Core.DomainObjects;

namespace NSE.Customer.API.Models;

public class Address : Entity
{
    public Address(string street, string number, string additionalInfo, string postalCode, string neighborhood, string city, string state)
    {
        Street = street;
        Number = number;
        AdditionalInfo = additionalInfo;
        PostalCode = postalCode;
        Neighborhood = neighborhood;
        City = city;
        State = state;
    }

    public string Street { get; private set; }
    public string Number { get; private set; }
    public string AdditionalInfo { get; private set; }
    public string PostalCode { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public Guid CustomerId { get; private set; }
    public Customer Customer { get; protected set; }
}