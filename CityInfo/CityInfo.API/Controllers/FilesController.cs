using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentProvider)
        {
            _fileExtensionContentProvider = fileExtensionContentProvider;
        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            var filePath = @"C:\Simeon\Programming\Pluralsight\CitiesApi.json";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

             if (!_fileExtensionContentProvider.TryGetContentType(filePath, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, contentType, Path.GetFileName(filePath));
        }
    }
}
