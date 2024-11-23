using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record ChangeChargeStationGroupInput(Guid ChargeStationId, ChargeStationChangeGroupDto Dto)
    :IUseCaseInput;
