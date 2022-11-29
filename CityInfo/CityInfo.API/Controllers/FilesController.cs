using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var filePath = @"C:\Simeon\Programming\Pluralsight\CitiesApi.json";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "text/plain ", Path.GetFileName(filePath));
        }
    }
}
