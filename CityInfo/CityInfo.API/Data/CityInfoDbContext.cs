namespace CityInfo.API.Data
{
    using CityInfo.API.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CityInfoDbContext : DbContext
    {
        public CityInfoDbContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfIntereset> PointOfInteresets { get; set; }
    }
}
