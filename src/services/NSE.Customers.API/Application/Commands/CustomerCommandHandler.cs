using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Models;

namespace NSE.Customers.API.Application.Commands;

public class CustomerCommandHandler : CommandHandler, IRequestHandler<CreateCustomerCommand, ValidationResult>
{
    private readonly ICustomersRepository _customersRepository;

    public CustomerCommandHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }

    public async Task<ValidationResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid()) return request.ValidationResult;

        var customer = new Customer(request.Id, request.Name, request.Email, request.DocumentNumber);
        
        var existingCustomer = await _customersRepository.GetByDocumentNumber(request.DocumentNumber);

        if (existingCustomer is not null)
        {
            AddError("Document number already exists");
            return ValidationResult;
        }
        
        _customersRepository.AddCustomer(customer);
        
        customer.AddEvent(new CustomerCreatedEvent(request.Id, request.Name, request.Email, request.DocumentNumber));

        return await SaveDataAsync(_customersRepository.UnitOfWork);
    }
}