using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.Queries;

public interface IAggregateQuery<TAggregate> : IQuery
    where TAggregate : AggregateRootBase
{
    public Guid AggregateId { get; }
}