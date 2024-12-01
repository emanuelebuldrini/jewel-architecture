using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public class AggregateExistsQueryHandler<TAggregate, TId, TQuery>(IRepository<TAggregate, TId> repo)
        : IAggregateExistsQueryHandler<TAggregate, TId, TQuery>
        where TAggregate : IAggregateRoot<TId>
        where TId : notnull
        where TQuery : AggregateExistsQuery<TAggregate, TId>, IAggregateQuery<TAggregate, TId>
{
    public async Task<bool> HandleAsync(TQuery query)
    {
        return await repo.ExistsAsync(query.AggregateId);
    }
}
