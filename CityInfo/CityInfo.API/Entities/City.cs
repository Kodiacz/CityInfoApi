using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Entities
{
    public class City
    {
        public City()
        {
            this.PointOfInteresets = new List<PointOfIntereset>();  
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<PointOfIntereset> PointOfInteresets { get; set; }
    }
}
