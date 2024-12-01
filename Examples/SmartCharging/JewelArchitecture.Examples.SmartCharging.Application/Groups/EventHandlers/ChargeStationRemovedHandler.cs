using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationRemovedHandler( IAggregateByIdQueryHandler<GroupAggregate, Guid, AggregateByIdQuery<GroupAggregate, Guid>> groupByIdQueryHandler,
    IAddOrReplaceAggregateCommandHandler<GroupAggregate, Guid> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationRemoved>
{
    public async Task HandleAsync(ChargeStationRemoved domainEvent)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new AggregateByIdQuery<GroupAggregate,Guid>(domainEvent.Group.Id));

        // Must remove the related group charge station reference.            
        group.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceAggregateCommand<GroupAggregate, Guid>(group));
    }
}
