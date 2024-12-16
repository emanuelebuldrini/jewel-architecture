using JewelArchitecture.Examples.SmartCharging.WebApiTest.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using JewelArchitecture.Examples.FunPokedex.Interface.Pokemon;
using JewelArchitecture.Examples.FunPokedex.Domain.Pokemon;
using Shouldly;

namespace JewelArchitecture.Examples.FunPokedex.Test.Pokemon
{
    public class PokemonControllerTest : PokedexTestBase
    {
        [Theory]
        [InlineData("mewtwo", 150, "rare", true,
            "It was created by a scientist after years of horrific gene splicing and DNA engineering experiments.")]
        [InlineData("clefairy", 35, "mountain", false,
            "Its magical and cute appeal has many admirers. It is rare and found only in certain areas.")]
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
        [InlineData("Pikachu", 25)]
        [InlineData("pikachu", 25)]
        [InlineData("pikAchu", 25)]
        public async Task GetPokemonAsync_CaseInsensitive(string name, int expectedId)
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
        }

        [Fact]
        public async Task GetPokemonAsync_NotFound()
        {
            // Arrange
            var controller = ServiceProvider!.GetRequiredService<PokemonController>();

            // Act
            var response = await controller.GetPokemonAsync("pincopallino");

            // Assert
            response.ShouldBeOfType<NotFoundResult>();            
        }

        [Theory]
        [InlineData("mewtwo", 150, "rare", true,
           "Created by a scientist after years of horrific gene splicing and dna engineering experiments, it was.")]
        [InlineData("diglett", 50, "cave", false,
            "On plant roots, lives about one yard underground where it feeds. Above ground, it sometimes appears.")]
        [InlineData("steelix", 208, "cave", false,
           "Thought its body transformed as a result of iron accumulating internally from swallowing soil, it is.")]
        [InlineData("clefairy", 35, "mountain", false,
            "Its magical and cute appeal hath many admirers. 't is rare and did find only in certain areas.")]
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