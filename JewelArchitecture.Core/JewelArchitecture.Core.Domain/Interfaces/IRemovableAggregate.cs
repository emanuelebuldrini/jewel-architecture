namespace JewelArchitecture.Core.Domain.Interfaces;

public interface IRemovableAggregate
{
    void Remove(bool isCascadeRemoval = false);
}
