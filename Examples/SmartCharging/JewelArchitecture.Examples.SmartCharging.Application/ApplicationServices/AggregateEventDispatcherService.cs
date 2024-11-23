using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;

public class AggregateEventDispatcherService<TAggregate>(IEventDispatcher dispatcher)
    where TAggregate : AggregateRootBase
{       
    public async Task DispatchAggregateEventsAsync(TAggregate aggregate)
    {
        foreach (var domainEvent in aggregate.RaisedEvents)
        {
            await DispatchEventAsync(domainEvent);
        }

        // Finally clean up dispatched events.
        aggregate.ClearEvents();
    }

    public async Task DispatchEventAsync(IDomainEvent domainEvent)
    {
        // Resolve the specific domain event type run-time to dispatch it to related event handlers.
        await dispatcher.DispatchAsync((dynamic)domainEvent);
    }
}
