﻿using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;

public record ChangeGroupCommand(Guid GroupId, ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;