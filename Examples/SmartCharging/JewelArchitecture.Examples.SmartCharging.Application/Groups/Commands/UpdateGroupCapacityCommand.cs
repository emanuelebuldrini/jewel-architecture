using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;

public record UpdateGroupCapacityCommand(AmpereUnit GroupCapacity, GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;