namespace CityInfo.API
{
    using CityInfo.API.Models;

    public class CitiesDataStore
    {
        private CitiesDataStore()
        {
            this.Cities = new List<CityDto>
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urabn park in the United States",
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire State Bulding",
                            Description = "a 102-story skyscrapper located in Midtown Manhattan",
                        },
                    },
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Cathedral of Our Lady",
                            Description = "A Gothic style cathedral, conceived by architects Jan And Piete",
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Antwer Central Station",
                            Description = "The finest example of railway architecture in Belgium",
                        },
                    },
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with the big tower",
                    PointsOfInterest = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Eiffel Tower",
                            Description = "A wrough iron littice tower on the Champ de Mars",
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "The Louvre",
                            Description = "The world's largest meseum",
                        },
                    },
                },
            };
        }

        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();
    }
}
