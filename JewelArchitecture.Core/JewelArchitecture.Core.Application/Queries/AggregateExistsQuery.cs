using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Queries;

public record AggregateExistsQuery<TAggregate>(Guid AggregateId) : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;