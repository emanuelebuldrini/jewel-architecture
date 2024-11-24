using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Groups.Dto
{
    public record GroupCreateDto
    {
        public required string Name { get; init; }

        [Range(1, int.MaxValue)]
        public int CapacityAmps { get; init; }
    }
}
