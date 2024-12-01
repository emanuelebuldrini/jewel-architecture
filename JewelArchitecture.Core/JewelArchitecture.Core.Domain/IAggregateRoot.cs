using System.Collections.ObjectModel;

namespace JewelArchitecture.Core.Domain
{
    public interface IAggregateRoot<TId> where TId : notnull
    {
        TId Id { get;}

        ReadOnlyCollection<IDomainEvent> RaisedEvents { get; }

        void ClearEvents();
    }
}
