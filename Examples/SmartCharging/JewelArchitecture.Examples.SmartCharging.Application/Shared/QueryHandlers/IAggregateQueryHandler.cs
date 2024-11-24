using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public interface IAggregateQueryHandler<TAggregate, TQuery, TResult> :
    IQueryHandler<TQuery, TResult>
    where TQuery : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;
