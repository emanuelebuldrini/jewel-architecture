using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record AddGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;
