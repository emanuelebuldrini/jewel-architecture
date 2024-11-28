using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Decorators;

public class AggregateCommandEventDispatcher<TCommand, TAggregate>(AggregateEventDispatcherService<TAggregate> dispatcherService,
    ICommandHandler<TCommand> decoratee)
    : IAggregateCommandHandler<TCommand, TAggregate>
    where TCommand : IAggregateCommand<TAggregate>
    where TAggregate : AggregateRootBase
{
    public async Task HandleAsync(TCommand cmd)
    {
        await decoratee.HandleAsync(cmd);

        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);
    }
}
