using JewelArchitecture.Examples.SmartCharging.Core.Groups;

namespace JewelArchitecture.Examples.SmartCharging.Application.Commands;

public record RemoveGroupCommand(GroupAggregate Aggregate)
    : IAggregateCommand<GroupAggregate>;