using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationGroupChangedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddOrReplaceGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationGroupChanged>
{
    public async Task HandleAsync(ChargeStationGroupChanged domainEvent)
    {
        // Remove first the old group reference.
        var oldGroup = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.OldGroup.Id));
        oldGroup.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(oldGroup));

        // Finally update the new group charge station reference.
        var newGroup = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.NewGroup.Id));
        newGroup.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(newGroup));
    }
}
