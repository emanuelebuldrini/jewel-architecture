using JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases.Input;
using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Core.Application.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases;

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
