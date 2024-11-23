using JewelArchitecture.Examples.SmartCharging.Core.ValueObjects;
using System.Text.Json.Serialization;

namespace JewelArchitecture.Examples.SmartCharging.Core.Entities;

public record ChargeStationConnectorEntity 
{
    [JsonConstructor]
    // The entity should be created within its aggregate boundary.
    internal ChargeStationConnectorEntity() {}

    public required ConnectorId Id { get; init; }
    public required AmpereUnit MaxCurrent { get; set; }        
}
