using Microsoft.EntityFrameworkCore;
using NSE.Core.DomainObjects;
using NSE.Core.Mediator;

namespace NSE.Customers.API.Extensions;

public static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.Events != null && x.Entity.Events.Any());
        
        var domainEvents = domainEntities.SelectMany(x => x.Entity.Events).ToList();
        
        domainEntities.ToList().ForEach(entity => entity.Entity.ClearEvents());

        var tasks = domainEvents
            .Select(async (domainEvent) =>
            {
                await mediator.PublishEvent(domainEvent);
            });
        
        await Task.WhenAll(tasks);
    }
}