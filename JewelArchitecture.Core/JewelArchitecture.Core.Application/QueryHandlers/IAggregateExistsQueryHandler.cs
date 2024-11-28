using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public interface IAggregateExistsQueryHandler<TAggregate, TQuery> :
    IAggregateQueryHandler<TAggregate, TQuery, bool>
    where TQuery : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;
