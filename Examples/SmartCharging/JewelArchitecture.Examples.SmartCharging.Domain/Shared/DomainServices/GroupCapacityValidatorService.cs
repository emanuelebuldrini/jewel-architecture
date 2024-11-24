using JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;
using JewelArchitecture.Examples.SmartCharging.Domain.Shared;

namespace JewelArchitecture.Examples.SmartCharging.Domain.Shared.DomainServices;

public class GroupCapacityValidatorService
{
    private const string CapacityValidationError = "The Group capacity must be greater or equal than " +
        "the sum of max current of all related connectors.";

    public (bool canUpdate, string? validationError) CanUpdateGroupCapacity(AmpereUnit editedGroupCapacity,
        IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>

        // Cast the sum to long to prevent an overflow exception.
        editedGroupCapacity.Value >= groupConnectors.Sum(c => (long)c.MaxCurrent.Value) ?

        (true, null) : (false, CapacityValidationError);

    public (bool canUpdate, string? validationError) CanUpdateChargeStationConnectorMaxCurrent(AmpereUnit editedMaxCurrent, AmpereUnit groupCapacity,
        ChargeStationConnectorEntity connectorToUpdate, IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>
        // Filter out the connector to update from the group connectors to sum the edited max current value.        
        GroupCapacityInvariant(GetMaxSumExceptConnector(groupConnectors, connectorToUpdate), editedMaxCurrent.Value, groupCapacity) ?
        (true, null) : (false, CapacityValidationError);

    public (bool canAdd, string? validationError) CanAddChargeStationConnectors(IReadOnlyCollection<(ConnectorId, AmpereUnit)> chargeStationConnectors,
        AmpereUnit groupCapacity, IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>
        GroupCapacityInvariant(GetMaxSum(groupConnectors), GetMaxSum(chargeStationConnectors), groupCapacity) ?
        (true, null) : (false, CapacityValidationError);

    // Passthrough for a collection of Connectors already existing.
    public (bool canAdd, string? validationError) CanAddChargeStationConnectors(IReadOnlyCollection<ChargeStationConnectorEntity> chargeStationConnectors,
        AmpereUnit groupCapacity, IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>
         CanAddChargeStationConnectors(chargeStationConnectors.Select(s => (s.Id, s.MaxCurrent)).ToList().AsReadOnly(), groupCapacity, groupConnectors);

    public (bool canAdd, string? validationError) CanAddChargeStationConnector(AmpereUnit addingConnectorMaxCurrent,
        AmpereUnit groupCapacity, IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>
        GroupCapacityInvariant(GetMaxSum(groupConnectors), addingConnectorMaxCurrent.Value, groupCapacity) ?
        (true, null) : (false, CapacityValidationError);

    private bool GroupCapacityInvariant(long currentMaxCurrentSum, long addingMaxCurrentSum, AmpereUnit GroupCapacity) =>
        currentMaxCurrentSum + addingMaxCurrentSum <= GroupCapacity.Value;

    private long GetMaxSum(IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors) =>
       GetMaxSum(groupConnectors.Select(s => (s.Id, s.MaxCurrent)).ToList().AsReadOnly());

    // GetMaxSum overload for Connectors not yet created.
    private long GetMaxSum(IReadOnlyCollection<(ConnectorId, AmpereUnit MaxCurrent)> groupConnectors) =>
       // Cast to long to avoid an overflow between integers.
       groupConnectors.Sum(c => (long)c.MaxCurrent.Value);


    private long GetMaxSumExceptConnector(IReadOnlyCollection<ChargeStationConnectorEntity> groupConnectors,
        ChargeStationConnectorEntity connectorToExclude) =>
        GetMaxSum(groupConnectors.Where(c => c.Id != connectorToExclude.Id).ToList().AsReadOnly());


}
