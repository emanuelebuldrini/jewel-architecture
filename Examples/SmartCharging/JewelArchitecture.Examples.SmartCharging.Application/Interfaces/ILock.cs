namespace JewelArchitecture.Examples.SmartCharging.Application.Interfaces
{
    public interface ILock : IDisposable
    {
        void Release();
    }
}
