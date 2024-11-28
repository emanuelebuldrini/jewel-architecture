namespace JewelArchitecture.Core.Application.Abstractions;

public interface ILock : IDisposable
{
    void Release();
}
