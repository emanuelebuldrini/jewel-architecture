using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record CreateChargeStationInput(string Name, GroupReference Group,
    IReadOnlyCollection<(ConnectorId ConnectorId, AmpereUnit MaxCurrent)> Connectors) : IUseCaseInput;