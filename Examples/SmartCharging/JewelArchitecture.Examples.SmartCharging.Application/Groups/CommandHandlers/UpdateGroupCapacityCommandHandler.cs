using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
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