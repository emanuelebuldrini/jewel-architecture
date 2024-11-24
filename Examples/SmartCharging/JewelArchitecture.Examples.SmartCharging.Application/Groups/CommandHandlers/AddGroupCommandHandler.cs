using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;

public class AddGroupCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<AddGroupCommand, GroupAggregate>
{
    public async Task HandleAsync(AddGroupCommand cmd)
    {
        var group = cmd.Aggregate;
        await groupRepo.AddOrReplaceAsync(group);
    }
}