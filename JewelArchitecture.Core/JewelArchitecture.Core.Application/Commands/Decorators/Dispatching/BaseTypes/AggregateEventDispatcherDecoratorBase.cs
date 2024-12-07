using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands.Decorators.Dispatching.BaseTypes;

public abstract class AggregateEventDispatcherDecoratorBase<TAggregate, TId, TCommand>
    (IAggregateCommandHandler<TAggregate, TId, TCommand> decoratee,
    AggregateEventDispatcherService<TAggregate, TId> dispatcherService)
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
    where TCommand : IAggregateCommand<TAggregate, TId>
{
    protected async Task Dispatch(TCommand cmd)
    {
        await decoratee.HandleAsync(cmd);
        // Dispatch events only after ensuring persistence.
        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);
    }
}
