using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups;

public record GroupAggregate : AggregateRootBase
{
    public required AmpereUnit Capacity
    {
        get => _capacity!;

        set
        {
            bool isUpdate = _capacity != null;

            _capacity = value;

            if (isUpdate)
            {
                RaisedEvents.Add(new GroupCapacityUpdated(Id, Capacity));
            }
        }
    }

    public List<ChargeStationReference> ChargeStations { get; init; } = [];

    private AmpereUnit? _capacity;

    public void Remove()
    {
        // Let application layer remove the related charge stations since they cannot exist without a group.
        foreach (var chargeStationId in ChargeStations)
        {
            RaisedEvents.Add(new ChargeStationCascadeRemoval(chargeStationId.Id));
        }

        // Application layer processes the removal of the group.
        RaisedEvents.Add(new GroupRemoved(Id));
    }
}
