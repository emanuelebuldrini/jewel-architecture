using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;

public class GroupByIdQueryHandler(IRepository<GroupAggregate> groupRepo)
    : IQueryHandler<GroupByIdQuery, GroupAggregate>
{
    public async Task<GroupAggregate> HandleAsync(GroupByIdQuery query)
    {
        return await groupRepo.GetSingleAsync(query.GroupId);
    }
}
