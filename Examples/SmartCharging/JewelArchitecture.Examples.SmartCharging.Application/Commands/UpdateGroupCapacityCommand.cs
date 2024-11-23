using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record UpdateGroupCapacityCommand(AmpereUnit GroupCapacity, GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;