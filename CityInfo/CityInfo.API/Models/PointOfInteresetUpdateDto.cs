﻿namespace CityInfo.API.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PointOfInteresetUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
