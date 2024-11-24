using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

public interface IAggregateQuery<TAggregate> : IQuery
    where TAggregate : AggregateRootBase
{
    public Guid AggregateId { get; }
}