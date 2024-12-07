using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands.Decorators.Dispatching.BaseTypes;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands.Decorators.Dispatching;

public sealed class AggregateCommandEventDispatcher<TAggregate, TId, TCommand>
    (AggregateEventDispatcherService<TAggregate, TId> dispatcherService,
    IAggregateCommandHandler<TAggregate, TId, TCommand> decoratee)
    : AggregateEventDispatcherDecoratorBase<TAggregate, TId, TCommand>
    (decoratee, dispatcherService),
    IAggregateCommandHandler<TAggregate, TId, TCommand>
    where TAggregate : IAggregateRoot<TId>
    where TCommand : IAggregateCommand<TAggregate, TId>
    where TId : notnull
{
    public async Task HandleAsync(TCommand cmd) =>
        await Dispatch(cmd);
}
