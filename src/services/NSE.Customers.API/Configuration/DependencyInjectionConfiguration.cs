using FluentValidation.Results;
using MediatR;
using NSE.Core.Mediator;
using NSE.Customers.API.Application.Commands;
using NSE.Customers.API.Application.Events;
using NSE.Customers.API.Data;
using NSE.Customers.API.Data.Repositories;
using NSE.Customers.API.Models;
using NSE.Customers.API.Services;

namespace NSE.Customers.API.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<CustomersContext>();
        services.AddScoped<ICustomersRepository, CustomersRepository>();
        
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<CreateCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        services.AddScoped<INotificationHandler<CustomerCreatedEvent>, CustomerEventHandler>();
    }
}