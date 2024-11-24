using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases.Input;

public record RemoveGroupInput(Guid GroupId)
    : IUseCaseInput;
