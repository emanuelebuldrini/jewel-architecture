namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record RemoveGroupInput(Guid GroupId)
    : IUseCaseInput;
