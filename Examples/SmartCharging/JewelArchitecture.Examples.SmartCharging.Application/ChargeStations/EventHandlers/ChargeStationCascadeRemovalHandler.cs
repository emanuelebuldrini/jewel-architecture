using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.Events;
using JewelArchitecture.Core.Application.CommandHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.EventHandlers
{
    public class ChargeStationCascadeRemovalHandler(
        IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationQueryHandler,
        ICommandHandler<RemoveChargeStationCommand> removeChargeStationCommandHandler)
        : IEventHandler<ChargeStationCascadeRemoval>
    {
        public async Task HandleAsync(ChargeStationCascadeRemoval domainEvent)
        {
            var query = new ChargeStationByIdQuery(domainEvent.ChargeStationId);
            var chargeStation = await chargeStationQueryHandler.HandleAsync(query);

            // In this case remove without event dispatching to avoid loops with the group aggregate.
            await removeChargeStationCommandHandler.HandleAsync(new RemoveChargeStationCommand(chargeStation));
        }
    }
}
