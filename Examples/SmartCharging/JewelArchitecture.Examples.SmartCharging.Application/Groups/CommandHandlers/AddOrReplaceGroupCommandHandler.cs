using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.CommandHandlers;

public class AddOrReplaceGroupCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<AddOrReplaceGroupCommand, GroupAggregate>
{
    public async Task HandleAsync(AddOrReplaceGroupCommand cmd)
    {
        var group = cmd.Aggregate;
        await groupRepo.AddOrReplaceAsync(group);
    }
}