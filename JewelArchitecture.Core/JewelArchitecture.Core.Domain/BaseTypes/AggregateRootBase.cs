using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using JewelArchitecture.Core.Domain.Interfaces;

namespace JewelArchitecture.Core.Domain.BaseTypes;

public abstract record AggregateRootBase<TId> : IAggregateRoot<TId>
    where TId : notnull
{
    protected List<IDomainEvent> Events { get; } = [];

    public abstract TId Id
    {
        get;
        init;
    }

    [JsonIgnore]
    public ReadOnlyCollection<IDomainEvent> RaisedEvents => Events.AsReadOnly();

    public void ClearEvents()
    {
        Events.Clear();
    }
}