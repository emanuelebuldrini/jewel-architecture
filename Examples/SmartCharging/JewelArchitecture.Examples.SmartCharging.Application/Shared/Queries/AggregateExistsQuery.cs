using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;

public record AggregateExistsQuery<TAggregate>(Guid AggregateId) : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;