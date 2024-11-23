using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveChargeStationCommand(ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;