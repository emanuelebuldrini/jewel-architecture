using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Commands;

public interface IAggregateCommandHandler<TCommand, TAggregate> :
    ICommandHandler<TCommand>
    where TCommand : IAggregateCommand<TAggregate>
    where TAggregate : AggregateRootBase;
