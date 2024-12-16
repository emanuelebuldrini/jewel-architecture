using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.Resilience;

public class RetryPolicyOptions
{
    [Required]
    [Range(1, int.MaxValue)]
    public int RetryCount { get; set; }

    [Required]
    [Range(0, double.MaxValue)]
    public double BaseDelaySeconds { get; set; }

    [Required]
    public BackoffStrategy BackoffStrategy { get; set; }
}