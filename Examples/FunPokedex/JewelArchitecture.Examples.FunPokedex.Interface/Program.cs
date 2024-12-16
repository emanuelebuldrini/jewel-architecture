using JewelArchitecture.Core.Interface.Extensions;
using Microsoft.OpenApi.Models;
using JewelArchitecture.Examples.FunPokedex.Interface.Pokemon.Examples;
using JewelArchitecture.Examples.FunPokedex.Interface.Shared;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Fun Pokédex",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(AppContext.BaseDirectory, "JewelArchitecture.Examples.FunPokedex.Interface.xml");
    c.IncludeXmlComments(filePath);
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<PokemonExample>();

builder.Services.AddJewelArchitecture();
builder.Services.AddPokedex();

// Add external API clients to get Pokemon's data and related fun translations.
var pokedexApiConfig = builder.Configuration.GetSection("Pokeapi");
builder.Services.AddPokeapiClient(pokedexApiConfig);

var funTranslationsApiConfig = builder.Configuration.GetSection("FuntranslationsApi");
builder.Services.AddFuntranslationsClient(funTranslationsApiConfig);

var retryPolicyConfig = builder.Configuration.GetSection("RetryPolicy");
builder.Services.AddRetryPolicyOptions(retryPolicyConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
