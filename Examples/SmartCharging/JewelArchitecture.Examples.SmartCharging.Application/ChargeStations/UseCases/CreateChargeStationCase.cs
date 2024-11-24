using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;
using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Queries.Results;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.QueryHandlers;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases;

public class CreateChargeStationCase(ILockService<ChargeStationAggregate> chargeStationLockService,
    ILockService<GroupAggregate> groupLockService,
    IQueryHandler<GroupConnectorQuery, GroupConnectorResult> groupConnectorQueryHandler,
    IAggregateCommandHandler<AddChargeStationCommand, ChargeStationAggregate> addChargeStationCommandHandler,
    GroupCapacityValidatorService groupCapacityValidator) : IUseCase<CreateChargeStationInput, Guid>
{
    public async Task<Guid> HandleAsync(CreateChargeStationInput input)
    {
        var groupId = input.Group.Id;

        using var groupLock = await groupLockService.AcquireLockAsync();
        using var chargeStationLock = await chargeStationLockService.AcquireLockAsync();

        // Retrieve all group connectors to validate the group capacity.
        var result = await groupConnectorQueryHandler.HandleAsync(new GroupConnectorQuery(groupId));

        // Let the domain service decide if the charge station with related connectors can be added.
        var (canAddConnectors, validationError) = groupCapacityValidator.CanAddChargeStationConnectors(input.Connectors,
            result.Group.Capacity,
            result.Connectors);

        if (!canAddConnectors)
        {
            throw new ExceedsGroupCapacityException(validationError);
        }

        var chargeStation = ChargeStationAggregate.Create(input.Name, input.Connectors,
            new GroupReference(groupId));
        var command = new AddChargeStationCommand(chargeStation);

        await addChargeStationCommandHandler.HandleAsync(command);

        return chargeStation.Id;
    }
}
