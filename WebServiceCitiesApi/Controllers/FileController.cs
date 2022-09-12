using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebServiceCitiesApi.Controllers
{
    [ApiController]//not really necessary but improves developer xperience
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _FileExtensionContentTypeProvider;
        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _FileExtensionContentTypeProvider = fileExtensionContentTypeProvider ?? throw new System.ArgumentNullException("filecontentType provider is null");
        }
    
        [HttpGet("{fileName}")]
        public ActionResult GetFile(String fileName)
        {
            if (!System.IO.File.Exists(fileName)) return NotFound();

            if (!_FileExtensionContentTypeProvider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";//dft mediatype for binary data
            }

            var bytes = System.IO.File.ReadAllBytes(fileName);
            return File(bytes, contentType, Path.GetFileName(fileName));
        
        }
    }
}
