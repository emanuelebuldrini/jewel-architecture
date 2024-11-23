using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

public record GroupReference
{
    public Guid Id { get; }

    public GroupReference(Guid id)
    {
        ValidatorHelper.ValidateGuid(id);

        Id = id;
    }
}
