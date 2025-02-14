using FluentValidation;
using NSE.Customers.API.Application.Commands;

namespace NSE.Customers.API.Application.Validations;

public class CreateCustomerValidation : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidation()
    {
        RuleFor(c => c.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Id is invalid");
        
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(c => c.Email)
            .Must(IsValidEmail)
            .WithMessage("Email is invalid");
        
        RuleFor(c => c.DocumentNumber)
            .Must(IsValidDocumentNumber)
            .WithMessage("Document number is invalid");
    }

    protected static bool IsValidDocumentNumber(string documentNumber)
    {
        return Core.DomainObjects.Cpf.Validate(documentNumber);
    }

    protected static bool IsValidEmail(string email)
    {
        return Core.DomainObjects.Email.Validate(email);
    }
}