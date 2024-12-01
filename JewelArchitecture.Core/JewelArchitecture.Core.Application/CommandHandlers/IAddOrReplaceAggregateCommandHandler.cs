using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public interface IAddOrReplaceAggregateCommandHandler<TAggregate, TId>
    : IAggregateCommandHandler<TAggregate, TId, AddAggregateCommand<TAggregate, TId>>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;