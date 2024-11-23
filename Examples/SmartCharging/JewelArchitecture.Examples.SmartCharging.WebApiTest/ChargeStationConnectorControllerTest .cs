using JewelArchitecture.Examples.SmartCharging.Application.Dto.ChargeStation.Connector;
using JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Core.Groups;
using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using JewelArchitecture.Examples.SmartCharging.WebApi.Controllers;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Factories;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.Mocks;
using JewelArchitecture.Examples.SmartCharging.WebApiTest.TestBases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace JewelArchitecture.Examples.SmartCharging.WebApiTest
{
    public class ChargeStationConnectorControllerTest : DiTestBase
    {
        [Fact]
        public async Task PostAsync_Success()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
               connectorNumber1: 1, maxCurrentAmps1: 50, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            var maxCurrentAmps = 70;
            var connectorNumber = 2;

            var newConnector = new ChargeStationConnectorCreateDto
            {
                Id = connectorNumber,
                MaxCurrentAmps = maxCurrentAmps
            };

            // Act
            var response = await controller.PostAsync(chargeStation1.Id, newConnector);

            // Assert
            response.ShouldBeOfType<CreatedAtActionResult>();

            var repoChargeStation = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation.ShouldNotBeNull();
            repoChargeStation.Connectors.Count.ShouldBe(2);
            var repoConnector = repoChargeStation.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber);
            repoConnector.ShouldNotBeNull();
            repoConnector.MaxCurrent.Value.ShouldBe(maxCurrentAmps);
        }

        [Fact]
        public async Task PostAsync_NotFound()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
               connectorNumber1: 1, maxCurrentAmps1: 50, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            var maxCurrentAmps = 70;
            var connectorNumber = 2;

            var newConnector = new ChargeStationConnectorCreateDto
            {
                Id = connectorNumber,
                MaxCurrentAmps = maxCurrentAmps
            };

            // Act
            var response = await controller.PostAsync(Guid.NewGuid(), newConnector);

            // Assert
            response.ShouldBeOfType<NotFoundResult>();

            var repoChargeStation = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation.ShouldNotBeNull();
            repoChargeStation.Connectors.Count.ShouldBe(1);
            var repoConnector = repoChargeStation.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber);
            repoConnector.ShouldBeNull();
        }

        [Fact]
        public async Task PostAsync_InvalidMaxCurrent()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
               connectorNumber1, maxCurrentAmps1, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            var connectorNumber2 = 2; var maxCurrentAmps2 = 250;
            var newConnector = new ChargeStationConnectorCreateDto
            {
                Id = connectorNumber2,
                MaxCurrentAmps = maxCurrentAmps2
            };

            // Act
            var response = await controller.PostAsync(chargeStation1.Id, newConnector);

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation.ShouldNotBeNull();
            // Connector 2 should not be added due to invalid max current.
            repoChargeStation.Connectors.Count.ShouldBe(1);
            // Check that the existing connector is still there.
            var repoConnector = repoChargeStation.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber1);
            repoConnector.ShouldNotBeNull();
            repoConnector.MaxCurrent.Value.ShouldBe(maxCurrentAmps1);
        }

        [Fact]
        public async Task PostAsync_InvalidConnectorId()
        {
            // Arrange                        
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var chargeStation1 = AggregateFactory.CreateChargeStationWithOneConnector(
               connectorNumber1, maxCurrentAmps1, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            var connectorNumber2 = 1; var maxCurrentAmps2 = 30;
            var newConnector = new ChargeStationConnectorCreateDto
            {
                Id = connectorNumber2,
                MaxCurrentAmps = maxCurrentAmps2
            };

            // Act
            var response = await controller.PostAsync(chargeStation1.Id, newConnector);

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            var repoChargeStation = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation.ShouldNotBeNull();
            // Connector 2 should not be added due to invalid max current.
            repoChargeStation.Connectors.Count.ShouldBe(1);
            // Check that the existing connector is still there.
            var repoConnector = repoChargeStation.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber1);
            repoConnector.ShouldNotBeNull();
            repoConnector.MaxCurrent.Value.ShouldBe(maxCurrentAmps1);
        }

        [Fact]
        public async Task GetAsync_Success()
        {
            // Arrange                                  
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);
            group1.ChargeStations.Add(new ChargeStationReference(chargeStation1.Id));

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.GetAsync(chargeStation1.Id, connectorNumber1);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            okResult!.Value.ShouldBeOfType<ChargeStationConnectorEntity>();
            var resultConnector = okResult!.Value as ChargeStationConnectorEntity;
            resultConnector.ShouldBe(resultConnector);
        }

        [Theory]
        [InlineData("29185315-fdf2-4f03-b383-9d33787f6ed6", 4)]
        [InlineData("07bdf6b2-4a56-47ed-b232-e61413569226", 2)]
        [InlineData("8ac12964-587b-4cb1-ae58-03bc89d2d8f4", 1)]
        public async Task GetAsync_NotFound(string chargeStationId, int connectorId)
        {
            // Arrange                                  
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);
            group1.ChargeStations.Add(new ChargeStationReference(chargeStation1.Id));

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.GetAsync(new Guid(chargeStationId), connectorId);

            // Assert
            response.ShouldBeOfType<NotFoundResult>();           
        }

        [Fact]
        public async Task PutMaxCurrentAsync_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 - 10;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);           
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();
            
            // Act
            var response = await controller.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                new ChargeStationConnectorUpdateMaxCurrentDto
                {
                    MaxCurrentAmps = modifiedMaxCurrentAmps
                });

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            // The first connector has a change on the max current
            var repoConnector1 = repoChargeStation1.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber1);
            repoConnector1.ShouldNotBeNull();
            repoConnector1.MaxCurrent.Value.ShouldBe(modifiedMaxCurrentAmps);
            // The second connector should be unaltered.
            var repoConnector2 = repoChargeStation1.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber2);
            repoConnector2.ShouldNotBeNull();
            repoConnector2.ShouldBe(chargeStation1.Connectors.Single(s => s.Id.Value == connectorNumber2));
        }

        [Fact]
        public async Task PutMaxCurrentAsync_InvalidValue()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 + 290;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.PutMaxCurrentAsync(chargeStation1.Id, connectorNumber1,
                new ChargeStationConnectorUpdateMaxCurrentDto
                {
                    MaxCurrentAmps = modifiedMaxCurrentAmps
                });

            // Assert
            response.ShouldBeOfType<BadRequestObjectResult>();
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            chargeStationRepoMock.Aggregates.Count.ShouldBe(1);
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            var repoConnector1 = repoChargeStation1.Connectors.SingleOrDefault(s => s.Id.Value == connectorNumber1);
            // Check that the max current was not modified.
            repoConnector1!.MaxCurrent.ShouldBe(new AmpereUnit(maxCurrentAmps1));
        }

        [Theory]
        [InlineData("29185315-fdf2-4f03-b383-9d33787f6ed6", 4)]
        [InlineData("07bdf6b2-4a56-47ed-b232-e61413569226", 3)]
        [InlineData("8ac12964-587b-4cb1-ae58-03bc89d2d8f4", 1)]
        public async Task PutMaxCurrentAsync_NotFound(string chargeStationId, int connectorId)
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var modifiedMaxCurrentAmps = maxCurrentAmps1 + 290;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.PutMaxCurrentAsync(new Guid(chargeStationId), connectorId,
                new ChargeStationConnectorUpdateMaxCurrentDto
                {
                    MaxCurrentAmps = modifiedMaxCurrentAmps
                });

            // Assert
            response.ShouldBeOfType<NotFoundResult>();           
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.DeleteAsync(chargeStation1.Id, connectorNumber1);

            // Assert
            response.ShouldBeOfType<NoContentResult>();
            // Check that the Connector 1 was removed and Connector 2 is still there.
            groupRepoMock.Aggregates.Count.ShouldBe(1);
            chargeStationRepoMock.Aggregates.Count().ShouldBe(1);
            var repoChargeStation1 = chargeStationRepoMock.Aggregates.SingleOrDefault(s => s.Id == chargeStation1.Id);
            repoChargeStation1.ShouldNotBeNull();
            repoChargeStation1.Connectors.Count.ShouldBe(1);
            // Check that Connector 1 was removed.
            repoChargeStation1.Connectors.ShouldAllBe(c => c.Id.Value != connectorNumber1);
        }

        [Theory]
        [InlineData("29185315-fdf2-4f03-b383-9d33787f6ed6", 5)]
        [InlineData("07bdf6b2-4a56-47ed-b232-e61413569226", 1)]
        [InlineData("8ac12964-587b-4cb1-ae58-03bc89d2d8f4", 2)]
        public async Task DeleteAsync_NotFound(string chargeStationId, int connectorId)
        {
            // Arrange                       
            var connectorNumber1 = 1; var maxCurrentAmps1 = 70;
            var connectorNumber2 = 2; var maxCurrentAmps2 = 90;
            var group1 = new GroupAggregate { Name = "Group 1", Capacity = new AmpereUnit(300) };
            var chargeStation1 = AggregateFactory.CreateChargeStationWithTwoConnectors(
                connectorNumber1, maxCurrentAmps1, connectorNumber2, maxCurrentAmps2, group1);

            var groupRepoMock = new RepositoryMock<GroupAggregate>([group1]);
            var chargeStationRepoMock = new RepositoryMock<ChargeStationAggregate>([chargeStation1]);
            InitScenario(groupRepoMock, chargeStationRepoMock);
            var controller = ServiceProvider!.GetRequiredService<ChargeStationConnectorController>();

            // Act
            var response = await controller.DeleteAsync(new Guid(chargeStationId), connectorId);

            // Assert
            response.ShouldBeOfType<NotFoundResult>();           
        }
    }
}