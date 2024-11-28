using JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto;
using JewelArchitecture.Core.Application.UseCases;

namespace JewelArchitecture.Examples.SmartCharging.Application.ChargeStations.UseCases.Input;

public record UpdateGroupCapacityInput(Guid GroupId, GroupUpdateCapacityDto Dto) : IUseCaseInput;