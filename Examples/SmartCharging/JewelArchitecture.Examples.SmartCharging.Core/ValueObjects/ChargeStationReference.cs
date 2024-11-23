using JewelArchitecture.Examples.SmartCharging.Core.Helpers;

namespace JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

public record ChargeStationReference
{
    public Guid Id { get; }

    public ChargeStationReference(Guid id)
    {
        ValidatorHelper.ValidateGuid(id);

        Id = id;
    }
}
