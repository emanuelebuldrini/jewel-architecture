using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class RemoveGroupCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<RemoveGroupCommand,GroupAggregate>
{
    public async Task HandleAsync(RemoveGroupCommand cmd)
    {
        var group = cmd.Aggregate;
        group.Remove();

        await groupRepo.RemoveAsync(group);
    }
}