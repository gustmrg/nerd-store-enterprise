using EasyNetQ;
using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;

namespace NSE.Customers.API.Services;

public class CreateCustomerIntegrationHandler : BackgroundService
{
    private IBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus = RabbitHutch.CreateBus("host=localhost:5672");
        
        _bus.Rpc.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request 
            => new ResponseMessage(await CreateCustomer(request)));
        
        return Task.CompletedTask;
    }

    private async Task<ValidationResult> CreateCustomer(CreatedUserIntegrationEvent message)
    {
        var customerCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.DocumentNumber);
        ValidationResult result;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            result = await mediator.Send(customerCommand);
        }

        return result;
    }
}