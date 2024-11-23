using JewelArchitecture.Examples.SmartCharging.Core.AggregateRoots;
using System.Text.Json;
using System.Text;

namespace JewelArchitecture.Examples.SmartCharging.Infrastructure.Serializers
{
    public class AggregateJsonSerializer<TAggregate> where TAggregate : AggregateRootBase
    {
        public async Task<string> SerializeAsync(TAggregate aggregate)
        {
            using var memoryStream = new MemoryStream();

            await JsonSerializer.SerializeAsync(memoryStream, aggregate);

            // Convert the memory stream to a JSON string
            memoryStream.Position = 0;
            using var reader = new StreamReader(memoryStream, Encoding.UTF8);

            return await reader.ReadToEndAsync();
        }

        public async Task<TAggregate> DeserializeAsync(string jsonString)
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));

            var aggregate = await JsonSerializer.DeserializeAsync<TAggregate>(memoryStream);

            return aggregate ?? throw new InvalidDataException($"Unable to deserialize the following JSON string" +
                    $" to an aggregate of type '{typeof(TAggregate)}': \"{jsonString}\"");
        }
    }
}