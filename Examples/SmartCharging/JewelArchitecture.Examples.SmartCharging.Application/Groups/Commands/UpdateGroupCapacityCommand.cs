using JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;

public record UpdateGroupCapacityCommand(AmpereUnit GroupCapacity, GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;