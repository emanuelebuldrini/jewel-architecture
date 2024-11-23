using System.Text.Json.Serialization;

namespace JewelArchitecture.Examples.SmartCharging.Core.Shared;

public abstract record AggregateRootBase
{
    private readonly Guid _id;
    private string _name;

    public Guid Id
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

    [JsonIgnore]
    public List<IDomainEvent> RaisedEvents { get; } = [];

    protected AggregateRootBase()
    {
        _id = Guid.NewGuid();
        _name = string.Empty;
    }

    public void ClearEvents()
    {
        RaisedEvents.Clear();
    }
}