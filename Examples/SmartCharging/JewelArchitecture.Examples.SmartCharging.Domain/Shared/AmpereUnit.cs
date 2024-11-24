namespace JewelArchitecture.Examples.SmartCharging.Domain.Shared;

public record AmpereUnit
{
    public AmpereUnit(int value)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(value, 1);

        Value = value;
    }

    public int Value { get; }
}
