using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationRemovedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    IAddOrReplaceAggregateCommandHandler<GroupAggregate, Guid> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationRemoved>
{
    public async Task HandleAsync(ChargeStationRemoved domainEvent)
    {
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.Group.Id));

        // Must remove the related group charge station reference.            
        group.ChargeStations.Remove(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddAggregateCommand<GroupAggregate, Guid>(group));
    }
}
