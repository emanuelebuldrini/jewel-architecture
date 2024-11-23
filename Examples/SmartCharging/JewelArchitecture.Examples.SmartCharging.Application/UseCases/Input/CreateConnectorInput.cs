using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record CreateConnectorInput(Guid ChargeStationId, ChargeStationConnectorCreateDto Dto) : IUseCaseInput;
