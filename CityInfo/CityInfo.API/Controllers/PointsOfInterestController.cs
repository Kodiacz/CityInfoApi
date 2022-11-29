namespace CityInfo.API.Controllers
{
    using CityInfo.API.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPoointsOfInterests(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{pointOfInterestId}")]
        public ActionResult<PointOfInterestDto> GetSinglePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);

        }
    }
}
