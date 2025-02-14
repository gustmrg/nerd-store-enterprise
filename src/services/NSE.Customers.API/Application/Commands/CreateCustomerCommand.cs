using FluentValidation;
using FluentValidation.Results;
using NSE.Core.Messages;
using NSE.Customers.API.Application.Validations;

namespace NSE.Customers.API.Application.Commands;

public class CreateCustomerCommand : Command
{
    public CreateCustomerCommand(Guid id, string name, string email, string documentNumber)
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

    public override bool IsValid()
    {
        ValidationResult = new CreateCustomerValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}