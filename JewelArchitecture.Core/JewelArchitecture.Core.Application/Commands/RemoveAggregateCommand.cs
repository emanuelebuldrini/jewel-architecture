using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands;

public record RemoveAggregateCommand<TAggregate, TId>(TAggregate Aggregate, bool IsCascadeRemoval = false)
    : IAggregateCommand<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;
