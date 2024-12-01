namespace JewelArchitecture.Core.Domain;

public interface IRemovableAggregate
{
    void Remove(bool isCascadeRemoval = false);
}
