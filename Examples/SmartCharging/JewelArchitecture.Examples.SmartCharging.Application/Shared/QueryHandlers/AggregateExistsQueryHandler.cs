using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

public class AggregateExistsQueryHandler<TAggregate, TQuery>(IRepository<TAggregate> repo)
        : IAggregateExistsQueryHandler<TAggregate, TQuery>
        where TQuery : IAggregateQuery<TAggregate>
        where TAggregate : AggregateRootBase
{
    public async Task<bool> HandleAsync(TQuery query)
    {
        return await repo.ExistsAsync(query.AggregateId);
    }
}
