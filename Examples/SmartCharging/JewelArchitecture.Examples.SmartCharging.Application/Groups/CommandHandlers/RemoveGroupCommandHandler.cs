using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;

public class RemoveGroupCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<RemoveGroupCommand, GroupAggregate>
{
    public async Task HandleAsync(RemoveGroupCommand cmd)
    {
        var group = cmd.Aggregate;
        group.Remove();

        await groupRepo.RemoveAsync(group);
    }
}