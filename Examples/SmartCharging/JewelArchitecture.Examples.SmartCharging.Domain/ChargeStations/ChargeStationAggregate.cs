using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainEvents;
using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations.DomainExceptions;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;
using System.Text.Json.Serialization;

namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

public record ChargeStationAggregate : AggregateRootBase
{
    private GroupReference? _group;
    private readonly List<ChargeStationConnectorEntity> _connectors = [];

    [JsonConstructor]
    private ChargeStationAggregate() { }

    public required GroupReference Group
    {
        get => _group!;

        init
        {
            _group = value;
        }
    }

    public required IReadOnlyCollection<ChargeStationConnectorEntity> Connectors
    {
        get => _connectors.AsReadOnly();

        init
        {
            if (value.Count < 1)
            {
                throw new ArgumentException(nameof(Connectors));
            }

            _connectors.AddRange(value);
        }
    }

    public static ChargeStationAggregate Create(string name,
        IReadOnlyCollection<(ConnectorId Id, AmpereUnit MaxCurrent)> connectors, GroupReference group)
    {
        var chargeStation = new ChargeStationAggregate
        {
            Name = name,

            Connectors = connectors.Select(s => new ChargeStationConnectorEntity
            {
                Id = s.Id,
                MaxCurrent = s.MaxCurrent
            })
            .ToList().AsReadOnly(),

            Group = group
        };

        chargeStation.RaisedEvents.Add(new ChargeStationCreated(chargeStation.Id, chargeStation.Group));

        return chargeStation;
    }

    public void UpdateConnector(ConnectorId connectorId, AmpereUnit maxCurrent)
    {
        var connector = Connectors.Single(s => s.Id == connectorId);
        connector.MaxCurrent = maxCurrent;
        RaisedEvents.Add(new ChargeStationConnectorMaxCurrentUpdated(Id, connector.Id, connector.MaxCurrent));
    }

    public void Remove(bool cascadeRemoval = false)
    {
        // Application layer processes the removal of the charge station.
        RaisedEvents.Add(new ChargeStationRemoved(Id, Group));

        // Related connectors are automatically removed since they are part of the charge station aggregate.
        foreach (var connector in _connectors)
        {
            RaisedEvents.Add(new ChargeStationConnectorRemoved(Id, Group, connector.Id));
        }
    }

    public void RemoveConnector(ConnectorId connectorId)
    {
        if (_connectors.Count == 1)
        {
            throw new OneConnectorRequiredException($"Unable to remove the connector with ID '{connectorId.Value}.'");
        }

        var connector = _connectors.Single(s => s.Id == connectorId);
        _connectors.Remove(connector);

        RaisedEvents.Add(new ChargeStationConnectorRemoved(Id, Group, connector.Id));
    }

    public void AddConnector(ConnectorId connectorId, AmpereUnit connectorMaxCurrent)
    {
        if (_connectors.Any(c => c.Id == connectorId))
        {
            throw new ConnectorIdNotUniqueException(connectorId);
        }

        var newConnector = new ChargeStationConnectorEntity
        {
            Id = connectorId,
            MaxCurrent = connectorMaxCurrent
        };

        _connectors.Add(newConnector);

        RaisedEvents.Add(new ChargeStationConnectorAdded(Id, newConnector.Id, newConnector.MaxCurrent));
    }

    public void ChangeGroup(Guid groupId)
    {
        var oldGroup = _group;

        _group = new GroupReference(groupId);

        RaisedEvents.Add(new ChargeStationGroupChanged(Id,
            oldGroup!, NewGroup: _group));
    }
}