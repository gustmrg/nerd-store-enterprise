using MediatR;

namespace NSE.Customers.API.Application.Events;

public class CustomerEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}