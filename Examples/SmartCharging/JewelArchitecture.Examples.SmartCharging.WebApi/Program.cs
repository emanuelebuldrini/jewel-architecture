using JewelArchitecture.Examples.SmartCharging.WebApi.ChargeStations.DtoExamples;
using JewelArchitecture.Examples.SmartCharging.WebApi.Shared;
using Microsoft.OpenApi.Models;
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
            Title = "Jewel Architecture - Smart Charging Example",
            Version = "v1"
        }
     );

    var filePath = Path.Combine(AppContext.BaseDirectory, "JewelArchitecture.Examples.SmartCharging.WebApi.xml");
    c.IncludeXmlComments(filePath);
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<ChargeStationCreateDtoExample>();

builder.Services.AddSmartCharging();
builder.Services.AddInMemoryJsonRepository();

builder.Services.AddMvc(options =>
{
    // Fix issue on created at result using the full name of the action with the async suffix.
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
