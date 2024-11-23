using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Dto.Group;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.ApplicationServices;

public class GroupService(ILockService<GroupAggregate> groupLockService,
        IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
        IAggregateExistsQueryHandler<GroupAggregate, AggregateExistsQuery<GroupAggregate>> groupExistsQueryHandler,
        ICommandHandler<AddGroupCommand> addOrReplaceGroupCommandHandler
)
{
    public async Task<Guid> CreateAsync(GroupCreateDto dto)
    {
        var group = new GroupAggregate
        {
            Name = dto.Name,
            Capacity = new AmpereUnit(dto.CapacityAmps)
        };

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(group));

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

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(group));
    }
}
