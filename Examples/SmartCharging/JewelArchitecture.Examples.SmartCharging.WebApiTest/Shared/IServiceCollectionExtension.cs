using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared
{
    internal static class IServiceCollectionExtension
    {
        internal static IServiceCollection AddRepositoryMocks(this IServiceCollection serviceCollection, IRepository<GroupAggregate, Guid> groupRepoMock,
            IRepository<ChargeStationAggregate, Guid> chargeStationRepoMock) => serviceCollection.AddSingleton(typeof(IRepository<GroupAggregate, Guid>), (_) => groupRepoMock)
                .AddSingleton(typeof(IRepository<ChargeStationAggregate, Guid>), (_) => chargeStationRepoMock);
    }
}
