using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

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
