using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Decorators;

public class AddOrReplaceAggregateCommandEventDispatcher<TAggregate, TId>(AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
         AddOrReplaceAggregateCommandHandler<TAggregate, TId> decoratee)
   : IAddOrReplaceAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    public async Task HandleAsync(AddOrReplaceAggregateCommand<TAggregate, TId> cmd)
    {
        await decoratee.HandleAsync(cmd);

        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);
    }
}
