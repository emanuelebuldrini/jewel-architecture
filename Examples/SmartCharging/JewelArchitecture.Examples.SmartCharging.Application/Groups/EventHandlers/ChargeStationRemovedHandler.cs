using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationRemovedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddOrReplaceGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationRemoved>
{
    public async Task HandleAsync(ChargeStationRemoved domainEvent)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.Group.Id));

        // Must remove the related group charge station reference.            
        group.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(group));
    }
}
