namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.Resilience;

public enum BackoffStrategy
{
    Linear,
    Exponential,
    Constant
}
