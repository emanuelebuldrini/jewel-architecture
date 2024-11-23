using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record ChangeGroupCommand(Guid GroupId, ChargeStationAggregate Aggregate) 
    : IAggregateCommand<ChargeStationAggregate>;