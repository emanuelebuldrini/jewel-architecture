using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record CreateConnectorInput(Guid ChargeStationId, ChargeStationConnectorCreateDto Dto) : IUseCaseInput;
