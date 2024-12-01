using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.EventHandlers;

public class ChargeStationCreatedHandler(IAggregateByIdQueryHandler<GroupAggregate, Guid, AggregateByIdQuery<GroupAggregate, Guid>> groupByIdQueryHandler,
     IAddOrReplaceAggregateCommandHandler<GroupAggregate, Guid> addOrReplaceGroupCommandHandler)
    : IEventHandler<ChargeStationCreated>
{
    public async Task HandleAsync(ChargeStationCreated domainEvent)
    {
        // Must update the related group reference.            
        var group = await groupByIdQueryHandler.HandleAsync(new AggregateByIdQuery<GroupAggregate, Guid>(domainEvent.Group.Id));
        group.ChargeStations.Add(new ChargeStationReference(domainEvent.ChargeStationId));

        await addOrReplaceGroupCommandHandler.HandleAsync(new AddAggregateCommand<GroupAggregate, Guid>(group));
    }
}
