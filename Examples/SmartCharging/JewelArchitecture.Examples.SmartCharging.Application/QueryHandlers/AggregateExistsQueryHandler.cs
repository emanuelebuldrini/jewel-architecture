using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

public class AggregateExistsQueryHandler<TAggregate,TQuery>(IRepository<TAggregate> repo)
        : IAggregateExistsQueryHandler<TAggregate, TQuery>
        where TQuery : IAggregateQuery<TAggregate>
        where TAggregate : AggregateRootBase
{
    public async Task<bool> HandleAsync(TQuery query)
    {
        return await repo.ExistsAsync(query.AggregateId);
    }   
}
