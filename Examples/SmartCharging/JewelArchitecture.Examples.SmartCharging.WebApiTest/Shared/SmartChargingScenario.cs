using JewelArchitecture.Core.Application.Abstractions;
using JewelArchitecture.Core.Test;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared
{
    internal static class SmartChargingScenario
    {
        internal static void InitScenario(IServiceCollection serviceCollection,
            IRepository<GroupAggregate> groupRepoMock,
        IRepository<ChargeStationAggregate>? chargeStationRepoMock = null)
        {
            chargeStationRepoMock ??= new RepositoryMock<ChargeStationAggregate>();

            serviceCollection.AddRepositoryMocks(groupRepoMock, chargeStationRepoMock);
        }
    }
}
