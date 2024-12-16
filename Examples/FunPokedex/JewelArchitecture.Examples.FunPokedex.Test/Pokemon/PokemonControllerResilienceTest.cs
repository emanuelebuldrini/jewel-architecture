using JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using JewelArchitecture.Examples.FunPokedex.Interface.Pokemon;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;
using Shouldly;

namespace JewelArchitecture.Examples.FunPokedex.Test.Pokemon
{
    public class PokemonControllerResilienceTest : PokedexResilienceTestBase
    {
        [Theory]        
        [InlineData("steelix", 208, "cave", false,
           "It is thought its body transformed as a result of iron accumulating internally from swallowing soil.")]
        public async Task GetPokemonAsync_Success(string name, int expectedId, string expectedHabitat,
            bool expectedIsLegendary, string expectedDescription)
        {
            // Arrange

            var controller = ServiceProvider!.GetRequiredService<PokemonController>();

            // Act
            var response = await controller.GetPokemonAsync(name);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            var pokemon = okResult!.Value as PokemonAggregate;
            pokemon!.Id.ShouldBe(expectedId);
            pokemon!.Name.ShouldBe(name);
            pokemon!.Description.ShouldBe(expectedDescription);
            pokemon!.Habitat.ShouldBe(expectedHabitat);
            pokemon!.IsLegendary.ShouldBe(expectedIsLegendary);
        }
        

        [Theory]
        [InlineData("mewtwo", 150, "rare", true,
           "Created by a scientist after years of horrific gene splicing and dna engineering experiments, it was.")]       
        public async Task GetPokemonTranslatedAsync_Success(string name, int expectedId, string expectedHabitat,
           bool expectedIsLegendary, string expectedDescription)
        {
            // Arrange
            var controller = ServiceProvider!.GetRequiredService<PokemonController>();

            // Act
            var response = await controller.GetPokemonTranslatedAsync(name);

            // Assert
            response.ShouldBeOfType<OkObjectResult>();
            var okResult = response as OkObjectResult;
            var pokemon = okResult!.Value as PokemonAggregate;
            pokemon!.Id.ShouldBe(expectedId);
            pokemon!.Name.ShouldBe(name);
            pokemon!.Description.ShouldBe(expectedDescription);
            pokemon!.Habitat.ShouldBe(expectedHabitat);
            pokemon!.IsLegendary.ShouldBe(expectedIsLegendary);
        }
    }
}