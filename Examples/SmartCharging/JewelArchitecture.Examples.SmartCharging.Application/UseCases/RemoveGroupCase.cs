using JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Application.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.QueryHandlers;
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases;

public sealed class RemoveGroupCase(ILockService<ChargeStationAggregate> chargeStationLockService,
        ILockService<GroupAggregate> groupLockService,
        IQueryHandler<GroupByIdQuery, GroupAggregate> groupByIdQueryHandler,
        IAggregateCommandHandler<RemoveGroupCommand, GroupAggregate> removeGroupCommandHandler)
       : NoOutputUseCase<RemoveGroupInput>
{
    protected override async Task HandleNoOutputAsync(RemoveGroupInput input)
    {
        using var groupLock = await groupLockService.AcquireLockAsync();
        // It is required to delete charge stations in cascade (managed in the handler).
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        var query = new GroupByIdQuery(input.GroupId);
        var group = await groupByIdQueryHandler.HandleAsync(query);
        var command = new RemoveGroupCommand(group);

        await removeGroupCommandHandler.HandleAsync(command);
    }
}
