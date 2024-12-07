using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands.Decorators;

public sealed class RemoveAggregateCommandEventDispatcher<TAggregate, TId>
    (AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
    RemoveAggregateCommandHandler<TAggregate, TId> decoratee)
    : AggregateEventDispatcherDecoratorBase<TAggregate, TId, RemoveAggregateCommand<TAggregate, TId>>
    (decoratee, dispatcherService),
    IRemoveAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>, IRemovableAggregate
    where TId : notnull
{
    public async Task HandleAsync(RemoveAggregateCommand<TAggregate, TId> cmd)
    {
        // The Aggregate decides which events to trigger depending on the cascade logic.
        cmd.Aggregate.Remove(cmd.IsCascadeRemoval);

        await Dispatch(cmd);
    }
}
