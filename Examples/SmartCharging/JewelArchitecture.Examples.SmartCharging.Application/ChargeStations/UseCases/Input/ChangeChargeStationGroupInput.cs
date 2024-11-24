using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record ChangeChargeStationGroupInput(Guid ChargeStationId, ChargeStationChangeGroupDto Dto)
    : IUseCaseInput;
