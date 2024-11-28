using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using JewelArchitecture.Core.Application.Abstractions;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.ApplicationServices;

public class GroupService(ILockService<GroupAggregate> groupLockService,
        IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
        IAggregateExistsQueryHandler<GroupAggregate, AggregateExistsQuery<GroupAggregate>> groupExistsQueryHandler,
        ICommandHandler<AddOrReplaceGroupCommand> addOrReplaceGroupCommandHandler
)
{
    public async Task<Guid> CreateAsync(GroupCreateDto dto)
    {
        var group = new GroupAggregate
        {
            Name = dto.Name,
            Capacity = new AmpereUnit(dto.CapacityAmps)
        };

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(group));

        return group.Id;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        var query = new AggregateExistsQuery<GroupAggregate>(id);
        return await groupExistsQueryHandler.HandleAsync(query);
    }

    public async Task<GroupAggregate> GetSingleAsync(Guid groupId)
    {
        return await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(groupId));
    }

    public async Task EditAsync(Guid id, GroupEditDto dto)
    {
        using var groupLock = await groupLockService.AcquireLockAsync();

        var group = await GetSingleAsync(id);
        group.Name = dto.Name;

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(group));
    }
}
