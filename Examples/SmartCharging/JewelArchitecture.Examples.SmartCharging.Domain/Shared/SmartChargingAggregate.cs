using JewelArchitecture.Core.Domain.BaseTypes;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Shared;

public abstract record SmartChargingAggregate : AggregateRootBase<Guid>, IRemovableAggregate
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

    public abstract void Remove(bool isCascadeRemoval = false);
}