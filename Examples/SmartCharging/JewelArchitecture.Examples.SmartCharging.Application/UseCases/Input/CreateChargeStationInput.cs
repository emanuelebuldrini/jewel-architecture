
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record CreateChargeStationInput(string Name, GroupReference Group,
    IReadOnlyCollection<(ConnectorId ConnectorId, AmpereUnit MaxCurrent)> Connectors) : IUseCaseInput;