using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Formats.Tar;
using System.IO.Packaging;
using System.Security.Cryptography;
using System.Security.Policy;
using Wedding.Data;
using Wedding.Data.Entities;

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

        private static string[] _imageMimetypes = {"image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml"};
        private static string[] _imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };
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

                var picture = new Picture()
                {
                    PictureId = Guid.NewGuid(),
                    FileSize = (uint)file.Length,
                    OriginalFileName = file.FileName,
                    FileHash = hash,
                    DateTimeUploadedUtc = DateTime.UtcNow,
                    FileName = file.FileName,
                    FilePath = "",
                    FileUrl = new Uri(""),
                };

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
            var fileName = $"{Guid.NewGuid()}{extension}";
            
            FileInfo dir = new FileInfo(Path.Combine(_environment.WebRootPath, "images"));
            dir.Directory.Create();

            // TODO: refactor this so that the DB aspect is in a seperate method.
            await using var context = await _contextFactory.CreateDbContextAsync();

            try
            {
                if (Array.IndexOf(_imageMimetypes, mimeType) >= 0 && (Array.IndexOf(_imageExt, extension) >= 0))
                {
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        var url = Url.Content($"~/images/{fileName}");

                        byte[] hash;
                        using (var md5 = MD5.Create())
                        {
                            using (var streamReader = new StreamReader(file.OpenReadStream()))
                            {
                                hash = md5.ComputeHash(streamReader.BaseStream);
                            }
                        }

                        var picture = new Picture()
                        {
                            PictureId = Guid.NewGuid(),
                            FileSize = (uint)file.Length,
                            OriginalFileName = file.FileName,
                            FileHash = hash,
                            DateTimeUploadedUtc = DateTime.UtcNow,
                            FileName = fileName,
                            FilePath = filePath,
                            FileUrl = new Uri(url),
                        };

                        context.Pictures.Add(picture);
                        await context.SaveChangesAsync();
                        return Ok(new { 
                            Url = url,
                            Id = picture.PictureId.ToString()
                        });
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

