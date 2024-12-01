using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IAggregateQueryHandler<TAggregate, TId, TQuery, TResult> :
    IQueryHandler<TQuery, TResult>
    where TQuery : IAggregateQuery<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId> where TId : notnull ;
