using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationCreatedHandler(IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
    ICommandHandler<AddOrReplaceGroupCommand> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationCreated>
{
    public async Task HandleAsync(ChargeStationCreated domainEvent)
    {
        // Must update the related group reference.            
        var group = await groupByIdQueryHandler.HandleAsync(new GroupByIdQuery(domainEvent.Group.Id));
        group.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddOrReplaceGroupCommand(group));
    }
}
