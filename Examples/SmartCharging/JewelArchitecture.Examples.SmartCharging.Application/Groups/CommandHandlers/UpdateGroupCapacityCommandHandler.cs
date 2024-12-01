using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;

public class UpdateGroupCapacityCommandHandler(IRepository<GroupAggregate, Guid> groupRepo)
    : IAggregateCommandHandler<GroupAggregate, Guid, UpdateGroupCapacityCommand<GroupAggregate>>
{
    public async Task HandleAsync(UpdateGroupCapacityCommand<GroupAggregate> cmd)
    {
        var group = cmd.Aggregate;
        group.Capacity = cmd.GroupCapacity;

        await groupRepo.AddOrReplaceAsync(group);
    }
}