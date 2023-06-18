using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Net;
using System.Security.Cryptography;
using Wedding.Data;
using Wedding.Data.Entities;
using Wedding.Services;

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
        private readonly IServiceProvider _serviceProvider;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        private static readonly string[] _imageMimetypes = {"image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml"};
        private static readonly string[] _imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };
        public ImageController(IWebHostEnvironment environment, IServiceProvider serviceProvider, IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _environment = environment;
            _serviceProvider = serviceProvider;
            _contextFactory = dbContextFactory;
        }

        [HttpPost("upload/multiple")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(IFormFile[] files)
        {
            Hashtable location = new Hashtable();
            var pictureService = _serviceProvider.GetService<PictureService>();
            await using var context = await _contextFactory.CreateDbContextAsync();
            foreach (var file in files)
            {
                var result = SaveFile(file);
                location.Add("location", result.ToString());

                byte[] hash;
                using (var md5 = MD5.Create())
                {
                    using (var streamReader = new StreamReader(file.OpenReadStream()))
                    {
                        hash = md5.ComputeHash(streamReader.BaseStream);
                    }
                }

                var picture = await pictureService.UploadImageAsync(file.OpenReadStream(), file.FileName);

                context.Pictures.Add(picture);
            }
            await context.SaveChangesAsync();
            return Json(location);
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> Image(IFormFile file)
        {
            return await SaveFile(file);
        }

        private async Task<IActionResult> SaveFile(IFormFile file)
        {
            var mimeType = file.ContentType;
            var extension = Path.GetExtension(file.FileName);

            var pictureService = _serviceProvider.GetService<IPictureService>();
            var picture = await pictureService.UploadImageAsync(file.OpenReadStream(), file.FileName);

            if (Array.IndexOf(_imageMimetypes, mimeType) >= 0 && (Array.IndexOf(_imageExt, extension) >= 0))
            {
                try
                {
                    return Ok(new
                    {
                        Url = picture.Permalink.ToString(),
                        Id = picture.PictureId.ToString()
                    });

                }

                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }
            else
            {
                return StatusCode(422, "Invalid file format");
            }

        }

        [Authorize]
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Download(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || fileName.Contains(".."))
            {
                return BadRequest("Invalid file name");
            }

            var fileId = Guid.Parse(Path.GetFileNameWithoutExtension(fileName));

            var pictureService = _serviceProvider.GetService<IPictureService>();

            var picture = await pictureService.GetPictureAsync(fileId);
            
            var fileService = _serviceProvider.GetService<IFileStorageService>();
            var valetUri = await fileService.GetFileValet(picture.FileUrl);

            return RedirectTemporary(valetUri.ToString());
        }

        private IActionResult RedirectTemporary(string location)
        {
            var response = new HttpResponseMessage(HttpStatusCode.TemporaryRedirect);
            response.Headers.Location = new Uri(location);
            return new ObjectResult(response);
        }
    }
}

