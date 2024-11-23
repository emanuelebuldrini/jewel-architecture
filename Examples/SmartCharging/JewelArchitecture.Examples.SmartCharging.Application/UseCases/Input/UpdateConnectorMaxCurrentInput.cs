using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record UpdateConnectorMaxCurrentInput(Guid ChargeStationId, ConnectorId ConnectorId,
        ChargeStationConnectorUpdateMaxCurrentDto Dto) : IUseCaseInput;