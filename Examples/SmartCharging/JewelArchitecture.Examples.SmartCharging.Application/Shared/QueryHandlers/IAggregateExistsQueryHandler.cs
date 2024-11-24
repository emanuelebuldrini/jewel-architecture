using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public interface IAggregateExistsQueryHandler<TAggregate, TQuery> :
    IAggregateQueryHandler<TAggregate, TQuery, bool>
    where TQuery : IAggregateQuery<TAggregate>
    where TAggregate : AggregateRootBase;
