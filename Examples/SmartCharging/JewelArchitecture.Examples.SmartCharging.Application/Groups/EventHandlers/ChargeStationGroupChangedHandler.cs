using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationGroupChangedHandler( IAggregateByIdQueryHandler<GroupAggregate, Guid, AggregateByIdQuery<GroupAggregate, Guid>> groupByIdQueryHandler,
     IAddOrReplaceAggregateCommandHandler<GroupAggregate, Guid> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationGroupChanged>
{
    public async Task HandleAsync(ChargeStationGroupChanged domainEvent)
    {
        // Remove first the old group reference.
        var oldGroup = await groupByIdQueryHandler.HandleAsync(new AggregateByIdQuery<GroupAggregate,Guid>(domainEvent.OldGroup.Id));
        oldGroup.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceAggregateCommand<GroupAggregate, Guid>(oldGroup));

        // Finally update the new group charge station reference.
        var newGroup = await groupByIdQueryHandler.HandleAsync(new AggregateByIdQuery<GroupAggregate,Guid>(domainEvent.NewGroup.Id));
        newGroup.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceAggregateCommand<GroupAggregate, Guid>(newGroup));
    }
}
