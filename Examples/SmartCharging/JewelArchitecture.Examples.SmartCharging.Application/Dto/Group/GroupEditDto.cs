﻿using System.ComponentModel.DataAnnotations;

namespace JewelArchitecture.Examples.SmartCharging.Application.Dto.Group
{
    public record GroupEditDto
    {
        [Required]
        public required string Name { get; set; }
    }
}
