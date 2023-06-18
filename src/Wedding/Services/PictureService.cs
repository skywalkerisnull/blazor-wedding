using Wedding.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Wedding.Data;
using SkiaSharp;
using DocumentFormat.OpenXml.Drawing;
using Path = System.IO.Path;
using Picture = Wedding.Data.Entities.Picture;

namespace Wedding.Services
{
    public class PictureService : IPictureService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;
        private readonly IServiceProvider _serviceProvider;

        public PictureService(IDbContextFactory<ApplicationDbContext> contextFactory, IWebHostEnvironment environment, HttpClient httpClient, IServiceProvider serviceProvider)
        {
            _contextFactory = contextFactory;
            _environment = environment;
            _httpClient = httpClient;
            _serviceProvider = serviceProvider;
        }

        public async Task<Picture> GetPictureAsync(Guid pictureId)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Pictures.FindAsync(pictureId);
        }

        public async Task<Picture> GetPictureAsync(Uri pictureUrl)
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Pictures.FirstOrDefaultAsync(p => p.FileUrl == pictureUrl);
        }

        public async Task<List<Picture>> GetAllPicturesAsync()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Pictures.ToListAsync();
        }

        public async Task<List<Uri>> GetAllPictureThumbnailUrls()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Pictures.Select(p => p.ThumbnailUrl).ToListAsync();
        }

        public async Task<List<Uri>> GetAllPictureUrls()
        {
            await using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Pictures.Select(p => p.FileUrl).ToListAsync();
        }

        public async Task DeletePictureAsync(Guid pictureId)
        {
            var picture = await GetPictureAsync(pictureId);
            if (picture == null) return;

            var fileService = _serviceProvider.GetService<IFileStorageService>();
            await fileService.DeleteFileAsync(picture.FileUrl);
            await fileService.DeleteFileAsync(picture.ThumbnailUrl);

            await using var context = await _contextFactory.CreateDbContextAsync();

            // Delete the record from the database
            context.Pictures.Remove(picture);
            await context.SaveChangesAsync();
        }

        public async Task<Picture> ReplacePicture(Guid originalPictureId, Guid replacementPictureId)
        {
            var originalPicture = await GetPictureAsync(originalPictureId);
            var replacementPicture = await GetPictureAsync(replacementPictureId);
            if (originalPicture == null || replacementPicture == null) return null;

            var fileService = _serviceProvider.GetService<IFileStorageService>();
            await fileService.DeleteFileAsync(originalPicture.FileUrl);
            await fileService.DeleteFileAsync(originalPicture.ThumbnailUrl);

            // Update the original record with the replacement data
            originalPicture.OriginalFileName = replacementPicture.OriginalFileName;
            originalPicture.FileHash = replacementPicture.FileHash;
            originalPicture.DateTimeUploadedUtc = replacementPicture.DateTimeUploadedUtc;
            originalPicture.FileName = replacementPicture.FileName;
            originalPicture.Permalink = replacementPicture.Permalink;
            originalPicture.FileUrl = replacementPicture.FileUrl;
            originalPicture.FileSize = replacementPicture.FileSize;
            originalPicture.ThumbnailUrl = replacementPicture.ThumbnailUrl;
            originalPicture.ThumbnailSize = replacementPicture.ThumbnailSize;
            originalPicture.PixelsX = replacementPicture.PixelsX;
            originalPicture.PixelsY = replacementPicture.PixelsY;

            // Save the changes to the database
            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Pictures.Update(originalPicture);
            await context.SaveChangesAsync();

            // Delete the replacement record from the database
            context.Pictures.Remove(replacementPicture);
            await context.SaveChangesAsync();

            return originalPicture;
        }

        public async Task<Picture> UploadImageAsync(Stream imageStream, string fileName, Guid? WeddingId = null)
        {
            if (WeddingId == null)
            {
                WeddingId = Guid.Parse("dd49bf5c-39a8-45f4-a636-a58bc846f7a3");
            }
            // Generate a unique file name and path
            var pictureId = Guid.NewGuid();
            var newFileName = $"{pictureId}{Path.GetExtension(fileName)}";
            var thumbnailFileName = $"{pictureId}_thumb{Path.GetExtension(fileName)}";

            // Save the image file to the server
            var permalink = new Uri($"api/image/{newFileName}");
            var fileUrl = new Uri($"{WeddingId}/{newFileName}");
            var thumbnailUrl = new Uri($"{WeddingId}/{thumbnailFileName}");

            // Get the file size and hash
            var fileSize = (uint)imageStream.Length;
            var fileHash = await GetFileHashAsync(ref imageStream);

            // Generate a thumbnail image and save it to the server
            
            Stream thumbnailStream;
            GenerateThumbnailAsync(ref imageStream, out thumbnailStream);

            // Get the thumbnail size
            var thumbnailSize = (uint)thumbnailStream.Length;

            // Get the image dimensions
            var imageDimensions = await GetImageDimensionsAsync(ref imageStream);
            var pixelsX = imageDimensions.Item1;
            var pixelsY = imageDimensions.Item2;

            var fileService = _serviceProvider.GetService<IFileStorageService>();
            await fileService.UploadFileAsync(fileUrl, imageStream);
            await fileService.UploadFileAsync(thumbnailUrl, thumbnailStream);

            // Create a new picture record and save it to the database
            var picture = new Picture
            {
                PictureId = pictureId,
                OriginalFileName = fileName,
                FileHash = fileHash,
                DateTimeUploadedUtc = DateTime.UtcNow,
                FileName = newFileName,
                FileUrl = fileUrl, // update to the blobs
                Permalink = permalink,
                FileSize = fileSize,
                ThumbnailUrl = thumbnailUrl, // update to the blobs
                ThumbnailSize = thumbnailSize,
                PixelsX = pixelsX,
                PixelsY = pixelsY
            };

            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Pictures.Add(picture);
            await context.SaveChangesAsync();
            return picture;
        }

        private Task<byte[]> GetFileHashAsync(ref Stream inputStream)
        {
            // Use a hash algorithm to compute the hash of the file
            inputStream.Seek(0, SeekOrigin.Begin);
            using (var hashAlgorithm = System.Security.Cryptography.SHA256.Create())
            {
                return hashAlgorithm.ComputeHashAsync(inputStream);
            }
        }
        private static void GenerateThumbnailAsync(ref Stream inputStream, out Stream thumbnailStream, int outputWidth = 200, int outputHeight = 200, int outputQuality = 80)
        {
            thumbnailStream = new MemoryStream();
            inputStream.Seek(0, SeekOrigin.Begin);
            // Load the bitmap from the input stream
            var bitmap = SKBitmap.Decode(inputStream);

            // Calculate the new dimensions
            var width = outputWidth;
            var height = outputHeight;
            var aspectRatio = (float)bitmap.Width / bitmap.Height;
            if (aspectRatio > 1)
            {
                // Landscape
                height = (int)(width / aspectRatio);
            }
            else
            {
                // Portrait
                width = (int)(height * aspectRatio);
            }

            // Resize the bitmap
            var resizedBitmap = bitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);

            // Encode and save the thumbnail image
            var image = SKImage.FromBitmap(resizedBitmap);
            var data = image.Encode(SKEncodedImageFormat.Jpeg, outputQuality);
            data.SaveTo(thumbnailStream);
        }

        private Task<Tuple<uint, uint>> GetImageDimensionsAsync(ref Stream inputStream)
        {
            inputStream.Seek(0, SeekOrigin.Begin);
            // Load the bitmap from the input stream
            var bitmap = SKBitmap.Decode(inputStream);

            // Return the dimensions as a tuple
            return Task.FromResult(new Tuple<uint, uint>((uint)bitmap.Width, (uint)bitmap.Height));
        }

        public async Task UpdatePictureAsync(Picture picture)
        {
            // Check if the picture exists in the database
            var existingPicture = await GetPictureAsync(picture.PictureId);
            if (existingPicture == null) return;

            // Update the picture record with the new data
            existingPicture.AlternativeText = picture.AlternativeText;
            existingPicture.FileDescription = picture.FileDescription;
            await using var context = await _contextFactory.CreateDbContextAsync();

            // Save the changes to the database
            context.Pictures.Update(existingPicture);
            await context.SaveChangesAsync();
        }

    }
}