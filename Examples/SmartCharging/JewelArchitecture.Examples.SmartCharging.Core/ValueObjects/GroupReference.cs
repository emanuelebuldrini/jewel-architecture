using JewelArchitecture.Examples.SmartCharging.Core.Helpers;

namespace JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;

public record GroupReference
{
    public Guid Id { get; }

    public GroupReference(Guid id)
    {
        ValidatorHelper.ValidateGuid(id);

        Id = id;
    }
}
