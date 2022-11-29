namespace CityInfo.API.Controllers
{
    using CityInfo.API.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPoointsOfInterests(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            try
            {
                throw new Exception("Exception sample");

                if (city == null)
                {
                    this._logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
            }
            catch (Exception ex)
            {
                this._logger.LogCritical($"Exception while getting points of interest for city with id {cityId}", ex);

                return StatusCode(500, "A Problem happened while handling your request");
            }
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestEntity = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            return Ok(pointOfInterestEntity);

        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, [FromBody] PointOfInteresetForCreationDto pointOfIntereset)
        {
            //TODO: Thanks to the [ApiController] attribute this is done automatically so we don't need to use it
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfInterest).Max(p => p.Id);

            var PointsOfInterestEntity = new PointOfInterestDto
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfIntereset.Name,
                Description = pointOfIntereset.Description,
            };

            city.PointsOfInterest.Add(PointsOfInterestEntity);

            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, pointOfInterestId = PointsOfInterestEntity.Id }, PointsOfInterestEntity);
        }

        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestUpdateDto pointOfInteresetDto)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestEntity = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            pointOfInterestEntity.Name = pointOfInteresetDto.Name;
            pointOfInterestEntity.Description = pointOfInteresetDto.Description;

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestEntity = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInteresetToPatch = new PointOfInterestUpdateDto()
            {
                Name = pointOfInterestEntity.Name,
                Description = pointOfInterestEntity.Description,
            };

            patchDocument.ApplyTo(pointOfInteresetToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInteresetToPatch))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestEntity.Name = pointOfInteresetToPatch.Name;
            pointOfInterestEntity.Description = pointOfInteresetToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestEntity = city.PointsOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestEntity);

            return NoContent();
        }
    }
}
