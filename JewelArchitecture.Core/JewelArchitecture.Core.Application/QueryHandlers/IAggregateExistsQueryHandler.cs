using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IAggregateExistsQueryHandler<TAggregate, TId, TQuery> :
    IAggregateQueryHandler<TAggregate, TId, TQuery, bool>
    where TQuery : AggregateExistsQuery<TAggregate, TId>, IAggregateQuery<TAggregate, TId>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull;
