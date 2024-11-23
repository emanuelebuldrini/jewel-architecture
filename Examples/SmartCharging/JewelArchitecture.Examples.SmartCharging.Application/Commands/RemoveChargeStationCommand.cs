using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveChargeStationCommand(ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;