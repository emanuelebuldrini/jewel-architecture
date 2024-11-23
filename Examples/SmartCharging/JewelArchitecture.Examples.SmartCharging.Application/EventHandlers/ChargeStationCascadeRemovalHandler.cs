using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.DomainEvents;

namespace JewelArchitecture.Examples.SmartCharging.Application.EventHandlers
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
            
            await removeChargeStationCommandHandler.HandleAsync(new RemoveChargeStationCommand(chargeStation));
        }
    }
}
