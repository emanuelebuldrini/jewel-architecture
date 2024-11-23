using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.EventHandlers;

public class ChargeStationGroupChangedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationGroupChanged>
{
    public async Task HandleAsync(ChargeStationGroupChanged domainEvent)
    {
        // Remove first the old group reference.
        var oldGroup = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.OldGroup.Id));
        oldGroup.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));
        
        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(oldGroup));

        // Finally update the new group charge station reference.
        var newGroup = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.NewGroup.Id));
        newGroup.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));
        
        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(newGroup));
    }
}
