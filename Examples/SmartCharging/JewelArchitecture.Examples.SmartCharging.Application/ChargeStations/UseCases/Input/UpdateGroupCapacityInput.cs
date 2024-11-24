using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record UpdateGroupCapacityInput(Guid GroupId, GroupUpdateCapacityDto Dto) : IUseCaseInput;