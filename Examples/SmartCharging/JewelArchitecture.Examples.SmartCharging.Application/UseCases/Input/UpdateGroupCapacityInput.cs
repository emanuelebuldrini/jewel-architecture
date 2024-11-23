using JewelArchitecture.Examples.SmartCharging.Application.Dto.Group;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

public record UpdateGroupCapacityInput(Guid GroupId, GroupUpdateCapacityDto Dto) : IUseCaseInput;