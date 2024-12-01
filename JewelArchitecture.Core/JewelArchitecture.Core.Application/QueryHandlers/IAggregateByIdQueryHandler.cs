using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IAggregateByIdQueryHandler<TAggregate, TId, TQuery>
    : IAggregateQueryHandler<TAggregate, TId, TQuery, TAggregate>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
    where TQuery : AggregateByIdQuery<TAggregate, TId>, IAggregateQuery<TAggregate, TId>;