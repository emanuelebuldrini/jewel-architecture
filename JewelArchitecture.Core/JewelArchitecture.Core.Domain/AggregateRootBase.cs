using System.Text.Json.Serialization;

namespace JewelArchitecture.Core.Domain;

public abstract record AggregateRootBase
{
    public abstract Guid Id
    {
        get;
        init;       
    }

    [JsonIgnore]
    public List<IDomainEvent> RaisedEvents { get; } = [];

    public void ClearEvents()
    {
        RaisedEvents.Clear();
    }
}