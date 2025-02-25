using FluentValidation.Results;
using MediatR;
using NSE.Core.Messages.Integration;
using NSE.Customers.API.Application.Commands;
using NSE.MessageBus;

namespace NSE.Customers.API.Services;

public class CreateCustomerIntegrationHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageBus _messageBus;

    public CreateCustomerIntegrationHandler(IServiceProvider serviceProvider, IMessageBus messageBus)
    {
        _serviceProvider = serviceProvider;
        _messageBus = messageBus;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        
        return Task.CompletedTask;
    }

    private void SetResponder()
    {
        _messageBus.RespondAsync<CreatedUserIntegrationEvent, ResponseMessage>(async request 
            => await CreateCustomer(request));

        _messageBus.AdvancedBus.Connected += OnConnected;
    }

    private void OnConnected(object? sender, EventArgs e)
    {
        SetResponder();
    }

    private async Task<ResponseMessage> CreateCustomer(CreatedUserIntegrationEvent message)
    {
        var customerCommand = new CreateCustomerCommand(message.Id, message.Name, message.Email, message.DocumentNumber);
        ValidationResult result;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            result = await mediator.Send(customerCommand);
        }

        return new ResponseMessage(result);
    }
}