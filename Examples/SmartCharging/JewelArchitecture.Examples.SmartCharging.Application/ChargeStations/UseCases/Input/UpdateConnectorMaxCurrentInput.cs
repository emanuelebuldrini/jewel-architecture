using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record UpdateConnectorMaxCurrentInput(Guid ChargeStationId, ConnectorId ConnectorId,
        ChargeStationConnectorUpdateMaxCurrentDto Dto) : IUseCaseInput;