using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.QueryHandlers;

public class GroupByIdQueryHandler(IRepository<GroupAggregate> groupRepo)
    : IQueryHandler<GroupByIdQuery, GroupAggregate>
{
    public async Task<GroupAggregate> HandleAsync(GroupByIdQuery query)
    {
        return await groupRepo.GetSingleAsync(query.GroupId);
    }
}
