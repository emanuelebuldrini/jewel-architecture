namespace JewelArchitecture.Examples.SmartCharging.Domain.ChargeStations;

public record ConnectorId
{
    public ConnectorId(int value)
    {
        if (!Enumerable.Range(1, 5).Contains(value))
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        Value = value;
    }

    public int Value { get; }
}
