using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Application.Queries;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.EventHandlers
{
    public class ChargeStationCascadeRemovalHandler(
        IAggregateByIdQueryHandler<ChargeStationAggregate, Guid, AggregateByIdQuery<ChargeStationAggregate, Guid>> chargeStationQueryHandler,
        IRemoveAggregateCommandHandler<ChargeStationAggregate, Guid> removeChargeStationCommandHandler)
        : IEventHandler<ChargeStationCascadeRemoval>
    {
        public async Task HandleAsync(ChargeStationCascadeRemoval domainEvent)
        {
            var query = new AggregateByIdQuery<ChargeStationAggregate, Guid>(domainEvent.ChargeStationId);
            var chargeStation = await chargeStationQueryHandler.HandleAsync(query);

            // In this case the removal is done without parent event dispatching to avoid loops with the Group aggregate.
            var cmd = new RemoveAggregateCommand<ChargeStationAggregate, Guid>(chargeStation, IsCascadeRemoval: true);
            await removeChargeStationCommandHandler.HandleAsync(cmd);
        }
    }
}
