using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.EventHandlers;

public class ChargeStationRemovedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationRemoved>
{
    public async Task HandleAsync(ChargeStationRemoved domainEvent)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.Group.Id));

        // Must remove the related group charge station reference.            
        group.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(group));
    }
}
