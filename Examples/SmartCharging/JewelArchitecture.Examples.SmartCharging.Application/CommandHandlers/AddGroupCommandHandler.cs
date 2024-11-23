using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public class AddGroupCommandHandler(IRepository<GroupAggregate> groupRepo)
    : IAggregateCommandHandler<AddGroupCommand,GroupAggregate>
{
    public async Task HandleAsync(AddGroupCommand cmd)
    {
        var group = cmd.Aggregate;
        await groupRepo.AddOrReplaceAsync(group);        
    }
}