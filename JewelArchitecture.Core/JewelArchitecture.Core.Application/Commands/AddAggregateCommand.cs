using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Commands;

public record AddAggregateCommand<TAggregate, TId>(TAggregate Aggregate) 
    : IAggregateCommand<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId: notnull;
