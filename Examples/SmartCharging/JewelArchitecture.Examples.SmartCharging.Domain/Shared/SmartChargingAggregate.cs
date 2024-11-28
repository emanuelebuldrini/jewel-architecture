using JewelArchitecture.Core.Domain;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Shared;

public abstract record SmartChargingAggregate : AggregateRootBase
{
    private readonly Guid _id;
    private string _name;

    public override Guid Id
    {
        get => _id;

        init
        {
            ValidatorHelper.ValidateGuid(value);

            _id = value;
        }
    }

    public required string Name
    {
        get => _name;

        set
        {
            ValidatorHelper.ValidateName(value);

            _name = value;
        }
    }

    protected SmartChargingAggregate()
    {
        _id = Guid.NewGuid();
        _name = string.Empty;
    }
}