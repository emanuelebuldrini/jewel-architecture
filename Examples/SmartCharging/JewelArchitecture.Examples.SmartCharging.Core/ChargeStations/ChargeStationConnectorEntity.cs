using JewelArchitecture.Examples.SmartCharging.Core.Shared;
using System.Text.Json.Serialization;

namespace JewelArchitecture.Examples.SmartCharging.Core.ChargeStations;

public record ChargeStationConnectorEntity
{
    [JsonConstructor]
    // The entity should be created within its aggregate boundary.
    internal ChargeStationConnectorEntity() { }

    public required ConnectorId Id { get; init; }
    public required AmpereUnit MaxCurrent { get; set; }
}
