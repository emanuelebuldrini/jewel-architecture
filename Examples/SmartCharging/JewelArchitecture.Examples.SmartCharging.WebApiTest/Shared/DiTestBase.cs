using JewelArchitecture.Examples.SmartCharging.Application.Shared.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared
{
    public abstract class DiTestBase : IDisposable
    {
        private readonly ServiceCollection _serviceCollection;
        private ServiceProvider? _serviceProvider;

        protected ServiceProvider? ServiceProvider { get => _serviceProvider; }

        public DiTestBase()
        {
            _serviceCollection = ServiceCollectionFactory.GetSmartCharging();
        }

        protected void InitScenario(IRepository<GroupAggregate> groupRepoMock,
         IRepository<ChargeStationAggregate>? chargeStationRepoMock = null)
        {
            chargeStationRepoMock ??= new RepositoryMock<ChargeStationAggregate>();

            _serviceCollection.AddRepositoryMocks(groupRepoMock, chargeStationRepoMock);
            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public virtual void Dispose()
        {
            _serviceProvider?.Dispose();
        }
    }
}
