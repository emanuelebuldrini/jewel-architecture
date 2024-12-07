using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Test.Mocks;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.Test.Shared
{
    internal static class SmartChargingScenario
    {
        internal static void InitScenario(IServiceCollection serviceCollection,
            IRepository<GroupAggregate, Guid> groupRepoMock,
        IRepository<ChargeStationAggregate, Guid>? chargeStationRepoMock = null)
        {
            chargeStationRepoMock ??= new RepositoryMock<ChargeStationAggregate, Guid>();

            serviceCollection.AddRepositoryMocks(groupRepoMock, chargeStationRepoMock);
        }
    }
}
