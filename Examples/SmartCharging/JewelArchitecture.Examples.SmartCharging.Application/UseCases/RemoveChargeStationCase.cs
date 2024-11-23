using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;


namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases;

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
