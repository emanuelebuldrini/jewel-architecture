using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IAggregateQueryHandler<TAggregate, TQuery, TResult> :
    IQueryHandler<TQuery, TResult>
    where TQuery : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;
