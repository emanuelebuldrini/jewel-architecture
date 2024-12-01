using JewelArchitecture.Examples.SmartCharging.Domain.Groups.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups;

public record GroupAggregate : SmartChargingAggregate
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
                Events.Add(new GroupCapacityUpdated(Id, Capacity));
            }
        }
    }

    public List<ChargeStationReference> ChargeStations { get; init; } = [];

    private AmpereUnit? _capacity;

    public override void Remove(bool isCascadeRemoval = false)
    {
        // Let application layer remove the related charge stations since they cannot exist without a group.
        foreach (var chargeStationId in ChargeStations)
        {
            Events.Add(new ChargeStationCascadeRemoval(chargeStationId.Id));
        }

        // Application layer processes the removal of the group.
        Events.Add(new GroupRemoved(Id));
    }
}
