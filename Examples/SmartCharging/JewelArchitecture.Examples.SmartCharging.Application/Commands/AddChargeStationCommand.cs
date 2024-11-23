using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record AddChargeStationCommand(ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;
