using JewelArchitecture.Core.Application.Commands;
using JewelArchitecture.Core.Domain.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Commands;

public record UpdateGroupCapacityCommand<TAggregate>(AmpereUnit GroupCapacity, TAggregate Aggregate)
    : IAggregateCommand<TAggregate, Guid>
    where TAggregate : GroupAggregate, IAggregateRoot<Guid>;