using JewelArchitecture.Examples.SmartCharging.Core.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Core.Groups;

public record ChargeStationReference
{
    public Guid Id { get; }

    public ChargeStationReference(Guid id)
    {
        ValidatorHelper.ValidateGuid(id);

        Id = id;
    }
}
