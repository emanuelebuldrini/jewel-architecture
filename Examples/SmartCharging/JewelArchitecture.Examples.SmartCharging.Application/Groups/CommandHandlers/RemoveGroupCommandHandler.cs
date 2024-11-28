using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

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