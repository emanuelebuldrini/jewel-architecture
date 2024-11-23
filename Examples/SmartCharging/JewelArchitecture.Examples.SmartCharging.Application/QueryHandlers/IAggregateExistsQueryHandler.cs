using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public interface IAggregateExistsQueryHandler<TAggregate, TQuery> :
    IAggregateQueryHandler<TAggregate, TQuery, bool>
    where TQuery: IAggregateQuery<TAggregate>
    where TAggregate: AggregateRootBase;
