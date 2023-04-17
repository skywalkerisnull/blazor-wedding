using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Formats.Tar;

namespace Wedding.Controllers
{
    /// <summary>
    /// Created by Bing 16 April 2023
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        private static string[] _imageMimetypes = {"image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml"};
        private static string[] _imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };
        public ImageController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost("upload/multiple")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(IFormFile[] files)
        {
            Hashtable location = new Hashtable();

            foreach (var file in files)
            {
                var result = SaveFile(file);
                location.Add("location", result.ToString());
            }
            return Json(location);
        }

        [Authorize]
        [HttpPost("upload")]
        public IActionResult Image(IFormFile file)
        {
            return SaveFile(file);
        }

        private IActionResult SaveFile(IFormFile file)
        {
            // TODO: create an entry in the DB to allow for easy/quick tracking of the images that have been uploaded.
            var mimeType = file.ContentType;
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            
            FileInfo dir = new FileInfo(Path.Combine(_environment.WebRootPath, "images"));
            dir.Directory.Create();

            try
            {
                if (Array.IndexOf(_imageMimetypes, mimeType) >= 0 && (Array.IndexOf(_imageExt, extension) >= 0))
                {
                    using (var stream = new FileStream(Path.Combine(_environment.WebRootPath, "images", fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var url = Url.Content($"~/images/{fileName}");
                        return Ok(new { Url = url });
                    }
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{fileName}")]
        public IActionResult Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName.Contains(".."))
            {
                return BadRequest("Invalid file name");
            }

            var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("File not found");
            }

            return File(System.IO.File.OpenRead(filePath), "image/jpeg");
        }
    }
}

