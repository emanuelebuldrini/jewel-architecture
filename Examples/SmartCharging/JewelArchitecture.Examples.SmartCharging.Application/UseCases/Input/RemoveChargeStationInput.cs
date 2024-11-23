namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record RemoveChargeStationInput(Guid ChargeStationId)
    : IUseCaseInput;
