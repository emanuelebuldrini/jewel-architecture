using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;


namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public sealed class RemoveChargeStationCase(ILockService<ChargeStationAggregate> chargeStationLockService,
        ILockService<GroupAggregate> groupLockService,
        IQueryHandler<ChargeStationByIdQuery, ChargeStationAggregate> chargeStationByIdQueryHandler,
        IAggregateCommandHandler<RemoveChargeStationCommand, ChargeStationAggregate> removeChargeStationCommandHandler)
       : NoOutputUseCase<RemoveChargeStationInput>
{
    protected override async Task HandleNoOutputAsync(RemoveChargeStationInput input)
    {
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();
        // It is required to update the group reference as well (managed in the handler).
        using var groupLock = await groupLockService.AcquireLockAsync();

        var query = new ChargeStationByIdQuery(input.ChargeStationId);
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(query);
        var command = new RemoveChargeStationCommand(chargeStation);

        await removeChargeStationCommandHandler.HandleAsync(command);
    }
}
