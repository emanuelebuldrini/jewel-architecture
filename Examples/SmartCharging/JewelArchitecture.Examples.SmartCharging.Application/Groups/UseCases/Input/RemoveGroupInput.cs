using JewelArchitecture.Core.Application.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.UseCases.Input;

public record RemoveGroupInput(Guid GroupId)
    : IUseCaseInput;
