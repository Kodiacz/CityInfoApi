namespace CityInfo.API.Controllers
{
    using CityInfo.API.Models;
    using CityInfo.API.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository cityInfoRepo;

        public CitiesController(ICityInfoRepository cityInfoRepo)
        {
            this.cityInfoRepo = cityInfoRepo;
        }

        //TODO: [HttpGet("api/cities")] => this is used if we dont specify [Route("api/cities")] attribute on the top of the controller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInteresetDto>>> GetCities()
        {
            var cityEntities = await cityInfoRepo.GetCitiesAsync();

            var result = new List<CityWithoutPointsOfInteresetDto>();

            foreach (var cityEntity in cityEntities)
            {
                result.Add(new CityWithoutPointsOfInteresetDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name,
                });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }
    }
}