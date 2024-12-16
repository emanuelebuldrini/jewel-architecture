using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.FunPokedex.Infrastructure.ApiClients;

public class ApiClientOptions
{
    [Required]
    public required Uri BaseUrl { get; set; }
    public TimeSpan? CacheDuration { get; set; }
}
