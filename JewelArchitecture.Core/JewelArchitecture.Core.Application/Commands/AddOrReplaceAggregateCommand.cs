using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Application.Commands;

public record AddOrReplaceAggregateCommand<TAggregate, TId>(TAggregate Aggregate) 
    : IAggregateCommand<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId: notnull;
