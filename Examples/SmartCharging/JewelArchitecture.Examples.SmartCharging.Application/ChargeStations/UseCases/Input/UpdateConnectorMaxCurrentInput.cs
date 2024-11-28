using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Core.Application.UseCases;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record UpdateConnectorMaxCurrentInput(Guid ChargeStationId, ConnectorId ConnectorId,
        ChargeStationConnectorUpdateMaxCurrentDto Dto) : IUseCaseInput;