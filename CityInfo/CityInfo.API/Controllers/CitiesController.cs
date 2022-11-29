namespace CityInfo.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        //[HttpGet("api/cities")] => this is used if we dont specify [Route("api/cities")] attribute on the top of the controller
        [HttpGet]
        public JsonResult GetCities()
        {
            return new JsonResult(new List<object>
            {
                new { id = 1, Name = "New York City", },
                new { id = 1, Name = "Antwerp "},
            });
        }
    }
}
--====