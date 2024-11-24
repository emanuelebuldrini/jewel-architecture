using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Groups;

public record ChargeStationReference
{
    public Guid Id { get; }

    public ChargeStationReference(Guid id)
    {
        ValidatorHelper.ValidateGuid(id);

        Id = id;
    }
}
