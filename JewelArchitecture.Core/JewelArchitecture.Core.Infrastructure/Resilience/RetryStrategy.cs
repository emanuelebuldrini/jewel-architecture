namespace JewelArchitecture.Core.Infrastructure.Resilience;

public enum BackoffStrategy
{
    Linear,
    Exponential,
    Constant
}
