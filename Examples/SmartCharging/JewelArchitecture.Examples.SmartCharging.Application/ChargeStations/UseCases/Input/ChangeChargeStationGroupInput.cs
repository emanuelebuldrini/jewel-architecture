using JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.Dto;
using JewelArchitecture.Core.Application.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record ChangeChargeStationGroupInput(Guid ChargeStationId, ChargeStationChangeGroupDto Dto)
    : IUseCaseInput;
