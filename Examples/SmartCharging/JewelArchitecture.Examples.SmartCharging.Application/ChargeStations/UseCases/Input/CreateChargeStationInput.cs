using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record CreateChargeStationInput(string Name, GroupReference Group,
    IReadOnlyCollection<(ConnectorId ConnectorId, AmpereUnit MaxCurrent)> Connectors) : IUseCaseInput;