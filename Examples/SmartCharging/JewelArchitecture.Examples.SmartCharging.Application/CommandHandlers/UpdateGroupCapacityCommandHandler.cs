using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

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