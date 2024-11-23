using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.Group
{
    public record GroupCreateDto
    {
        public required string Name { get; init; }

        [Range(1, int.MaxValue)]
        public int CapacityAmps { get; init; }
    }
}
