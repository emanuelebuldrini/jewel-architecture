using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record RemoveChargeStationInput(Guid ChargeStationId)
    : IUseCaseInput;
