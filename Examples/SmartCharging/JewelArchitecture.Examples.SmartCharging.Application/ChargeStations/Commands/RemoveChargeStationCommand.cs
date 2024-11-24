﻿using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Commands;

public record RemoveChargeStationCommand(ChargeStationAggregate Aggregate)
    : IAggregateCommand<ChargeStationAggregate>;