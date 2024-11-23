using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public interface IAggregateQueryHandler<TAggregate, TQuery, TResult> :
    IQueryHandler<TQuery, TResult>
    where TQuery: IAggregateQuery<TAggregate>
    where TAggregate: AggregateRootBase;
