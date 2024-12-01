using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Core.Application.QueryHandlers;

public class AggregateByIdQueryHandler<TAggregate, TId, TQuery>(IRepository<TAggregate, TId> repo)
    : IAggregateByIdQueryHandler<TAggregate, TId, TQuery>
    where TAggregate : IAggregateRoot<TId>
    where TId : notnull
    where TQuery : AggregateByIdQuery<TAggregate, TId>, IAggregateQuery<TAggregate, TId>
{
    public async Task<TAggregate> HandleAsync(TQuery query) =>
        await repo.GetSingleAsync(query.AggregateId);
}
