using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands.Decorators.Dispatching.BaseTypes;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands.Decorators.Dispatching;

public sealed class AddOrReplaceAggregateCommandEventDispatcher<TAggregate, TId>
    (AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
    AddOrReplaceAggregateCommandHandler<TAggregate, TId> decoratee)
   : AggregateEventDispatcherDecoratorBase<TAggregate, TId, AddOrReplaceAggregateCommand<TAggregate, TId>>
    (decoratee, dispatcherService), IAddOrReplaceAggregateCommandHandler<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
{
    public async Task HandleAsync(AddOrReplaceAggregateCommand<TAggregate, TId> cmd) =>
        await Dispatch(cmd);
}
