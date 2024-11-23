using JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers.Decorators;

public class AggregateCommandEventDispatcher<TCommand, TAggregate>(AggregateEventDispatcherService<TAggregate> dispatcherService,
    ICommandHandler<TCommand> decoratee)
    : IAggregateCommandHandler<TCommand,TAggregate>
    where TCommand : IAggregateCommand<TAggregate>
    where TAggregate : AggregateRootBase
{
    public async Task HandleAsync(TCommand cmd)
    {
        await decoratee.HandleAsync(cmd);

        await dispatcherService.DispatchAggregateEventsAsync(cmd.Aggregate);
    }
}
