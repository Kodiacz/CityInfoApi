namespace CityInfo.API
{
    using CityInfo.API.Models;

    public class CitiesDataStore
    {
        public CitiesDataStore()
        {
            this.Cities = new List<CityDto>
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park",
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really fired",
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Paris",
                    Description = "The one with the big tower",
                },
            };
        }

        public List<CityDto> Cities { get; set; }
    }
}
