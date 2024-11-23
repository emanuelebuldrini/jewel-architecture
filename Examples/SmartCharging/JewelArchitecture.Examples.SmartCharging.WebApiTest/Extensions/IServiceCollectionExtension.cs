﻿using JewelArchitecture.Examples.SmartCharging.Application.Interfaces;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using Microsoft.Extensions.DependencyInjection;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest.Extensions
{
    internal static class IServiceCollectionExtension
    {
        internal static IServiceCollection AddRepositoryMocks(this IServiceCollection serviceCollection, IRepository<GroupAggregate> groupRepoMock,
            IRepository<ChargeStationAggregate> chargeStationRepoMock) => serviceCollection.AddSingleton(typeof(IRepository<GroupAggregate>), (_) => groupRepoMock)
                .AddSingleton(typeof(IRepository<ChargeStationAggregate>), (_) => chargeStationRepoMock);
    }
}