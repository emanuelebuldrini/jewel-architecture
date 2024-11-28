using JewelArchitecture.Core.Application.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record RemoveChargeStationInput(Guid ChargeStationId)
    : IUseCaseInput;
