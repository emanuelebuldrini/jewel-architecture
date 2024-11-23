using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.EventHandlers;

public class ChargeStationCreatedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationCreated>
{
    public async Task HandleAsync(ChargeStationCreated domainEvent)
    {
        // Must update the related group reference.            
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.Group.Id));
        group.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddGroupCommand(group));
    }
}
