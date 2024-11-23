using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Queries;

public record AggregateExistsQuery<TAggregate>(Guid AggregateId) : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;