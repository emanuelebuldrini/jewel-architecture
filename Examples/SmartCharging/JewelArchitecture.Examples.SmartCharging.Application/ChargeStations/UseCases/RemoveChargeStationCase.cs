using JewelArchitecture.Core.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;
using JewelArchitecture.Core.Application.CommandHandlers;
using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public sealed class RemoveChargeStationCase(ILockService<ChargeStationAggregate, Guid> chargeStationLockService,
        ILockService<GroupAggregate, Guid> groupLockService,
        IAggregateByIdQueryHandler<ChargeStationAggregate, Guid, AggregateByIdQuery<ChargeStationAggregate, Guid>> chargeStationByIdQueryHandler,
        IRemoveAggregateCommandHandler<ChargeStationAggregate, Guid> removeChargeStationCommandHandler)
       : NoOutputUseCase<RemoveChargeStationInput>
{
    protected override async Task HandleNoOutputAsync(RemoveChargeStationInput input)
    {
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();
        // It is required to update the group reference as well (managed in the handler).
        using var groupLock = await groupLockService.AcquireLockAsync();

        var query = new AggregateByIdQuery<ChargeStationAggregate, Guid>(input.ChargeStationId);
        var chargeStation = await chargeStationByIdQueryHandler.HandleAsync(query);
        var command = new RemoveAggregateCommand<ChargeStationAggregate, Guid>(chargeStation);

        await removeChargeStationCommandHandler.HandleAsync(command);
    }
}
