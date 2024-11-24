using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;

public class UpdateGroupCapacityCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<UpdateGroupCapacityCommand, GroupAggregate>
{
    public async Task HandleAsync(UpdateGroupCapacityCommand cmd)
    {
        var group = cmd.Aggregate;
        group.Capacity = cmd.GroupCapacity;

        await groupRepo.AddOrReplaceAsync(group);
    }
}