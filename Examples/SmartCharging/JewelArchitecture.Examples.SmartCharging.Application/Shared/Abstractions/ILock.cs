namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions
{
    public interface ILock : IDisposable
    {
        void Release();
    }
}
