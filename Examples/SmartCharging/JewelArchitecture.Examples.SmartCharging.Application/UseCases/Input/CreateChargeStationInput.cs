
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record CreateChargeStationInput(string Name, GroupReference Group,
    IReadOnlyCollection<(ConnectorId ConnectorId, AmpereUnit MaxCurrent)> Connectors) : IUseCaseInput;