using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public interface IRemoveAggregateCommandHandler<TAggregate, TId>
    : IAggregateCommandHandler<TAggregate, TId, RemoveAggregateCommand<TAggregate, TId>>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;