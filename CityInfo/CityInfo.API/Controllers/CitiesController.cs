namespace CityInfo.API.Controllers
{
    using CityInfo.API.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        //TODO: [HttpGet("api/cities")] => this is used if we dont specify [Route("api/cities")] attribute on the top of the controller
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
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