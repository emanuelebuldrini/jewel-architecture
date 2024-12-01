using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Decorators;

public class RemoveAggregateCommandEventDispatcher<TAggregate, TId>(AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
   RemoveAggregateCommandHandler<TAggregate, TId> decoratee)
    : IRemoveAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull 
{
    public async Task HandleAsync(RemoveAggregateCommand<TAggregate,TId> cmd)
    {
        cmd.Aggregate.Remove();

        // Dispatch events before physical removal.
        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);

        await decoratee.HandleAsync(cmd);
    }
}
