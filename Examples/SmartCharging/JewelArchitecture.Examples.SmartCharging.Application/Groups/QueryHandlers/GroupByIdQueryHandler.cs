using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.QueryHandlers;

public class GroupByIdQueryHandler(IRepository<GroupAggregate> groupRepo)
    : IQueryHandler<GroupByIdQuery, GroupAggregate>
{
    public async Task<GroupAggregate> HandleAsync(GroupByIdQuery query)
    {
        return await groupRepo.GetSingleAsync(query.GroupId);
    }
}
