using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public interface IAggregateCommandHandler<TCommand, TAggregate> :
    ICommandHandler<TCommand>
    where TCommand : IAggregateCommand<TAggregate>
    where TAggregate : AggregateRootBase;
