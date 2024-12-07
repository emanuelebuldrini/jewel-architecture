using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public interface IAggregateCommandHandler<TAggregate, TId, TCommand> :
    ICommandHandler<TCommand>
    where TCommand: IAggregateCommand<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;
