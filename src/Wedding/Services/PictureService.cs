using Wedding.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Wedding.Data;
using SkiaSharp;

namespace Wedding.Services
{
    public class PictureService : IPictureService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private readonly IWebHostEnvironment _environment;
        private readonly HttpClient _httpClient;

        public PictureService(IDbContextFactory<ApplicationDbContext> contextFactory, IWebHostEnvironment environment, HttpClient httpClient)
        {
            _contextFactory = contextFactory;
            _environment = environment;
            _httpClient = httpClient;
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

            // Delete the files from the server
            var filePath = Path.Combine(_environment.WebRootPath, picture.FilePath);
            var thumbnailPath = Path.Combine(_environment.WebRootPath, picture.ThumbnailFilePath);
            File.Delete(filePath);
            File.Delete(thumbnailPath);
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

            // Delete the original files from the server
            var originalFilePath = Path.Combine(_environment.WebRootPath, originalPicture.FilePath);
            var originalThumbnailPath = Path.Combine(_environment.WebRootPath, originalPicture.ThumbnailFilePath);
            File.Delete(originalFilePath);
            File.Delete(originalThumbnailPath);

            // Update the original record with the replacement data
            originalPicture.OriginalFileName = replacementPicture.OriginalFileName;
            originalPicture.FileHash = replacementPicture.FileHash;
            originalPicture.DateTimeUploadedUtc = replacementPicture.DateTimeUploadedUtc;
            originalPicture.FileName = replacementPicture.FileName;
            originalPicture.FilePath = replacementPicture.FilePath;
            originalPicture.FileUrl = replacementPicture.FileUrl;
            originalPicture.FileSize = replacementPicture.FileSize;
            originalPicture.ThumbnailFilePath = replacementPicture.ThumbnailFilePath;
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

        public async Task<Picture> UploadImageAsync(Stream imageStream, string fileName)
        {
            // Generate a unique file name and path
            var pictureId = Guid.NewGuid();
            var newFileName = $"{pictureId}{Path.GetExtension(fileName)}";
            var filePath = Path.Combine("images", newFileName);

            FileInfo dir = new FileInfo(Path.Combine(_environment.WebRootPath, "images"));
            dir.Directory.Create();

            // Save the image file to the server
            var fileUrl = new Uri($"{_environment.WebRootPath}/{filePath}");
            using (var fileStream = new FileStream(fileUrl.AbsolutePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Close();
                imageStream.Close();
            }

            // Get the file size and hash
            var fileSize = (uint)new FileInfo(fileUrl.AbsolutePath).Length;
            var fileHash = await GetFileHashAsync(fileUrl);

            // Generate a thumbnail image and save it to the server
            var thumbnailFileName = $"{pictureId}_thumb{Path.GetExtension(fileName)}";
            var thumbnailPath = Path.Combine("images", thumbnailFileName);
            var thumbnailUrl = new Uri($"{_environment.WebRootPath}/{thumbnailPath}");
            await GenerateThumbnailAsync(fileUrl, thumbnailUrl);

            // Get the thumbnail size
            var thumbnailSize = (uint)new FileInfo(thumbnailUrl.AbsolutePath).Length;

            // Get the image dimensions
            var imageDimensions = await GetImageDimensionsAsync(fileUrl);
            var pixelsX = imageDimensions.Item1;
            var pixelsY = imageDimensions.Item2;

            // Create a new picture record and save it to the database
            var picture = new Picture
            {
                PictureId = pictureId,
                OriginalFileName = fileName,
                FileHash = fileHash,
                DateTimeUploadedUtc = DateTime.UtcNow,
                FileName = newFileName,
                FilePath = filePath,
                FileUrl = fileUrl,
                FileSize = fileSize,
                ThumbnailFilePath = thumbnailPath,
                ThumbnailUrl = thumbnailUrl,
                ThumbnailSize = thumbnailSize,
                PixelsX = pixelsX,
                PixelsY = pixelsY
            };

            await using var context = await _contextFactory.CreateDbContextAsync();
            context.Pictures.Add(picture);
            await context.SaveChangesAsync();
            return picture;

            //if (Array.IndexOf(_imageMimetypes, mimeType) >= 0 && (Array.IndexOf(_imageExt, extension) >= 0))
            //{
            //    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        file.CopyTo(stream);
            //        var url = Url.Content($"~/images/{fileName}");

            //        byte[] hash;
            //        using (var md5 = MD5.Create())
            //        {
            //            using (var streamReader = new StreamReader(file.OpenReadStream()))
            //            {
            //                hash = md5.ComputeHash(streamReader.BaseStream);
            //            }
            //        }

            //        var picture = new Picture()
            //        {
            //            PictureId = Guid.NewGuid(),
            //            FileSize = (uint)file.Length,
            //            OriginalFileName = file.FileName,
            //            FileHash = hash,
            //            DateTimeUploadedUtc = DateTime.UtcNow,
            //            FileName = fileName,
            //            FilePath = filePath,
            //            FileUrl = new Uri(url),
            //        };

            //        context.Pictures.Add(picture);
            //        await context.SaveChangesAsync();
            //    }
        }

        private async Task<byte[]> GetFileHashAsync(Uri fileUrl)
        {
            // Use a hash algorithm to compute the hash of the file
            using (var hashAlgorithm = System.Security.Cryptography.SHA256.Create())
            {
                using (var fileStream = new FileStream(fileUrl.AbsolutePath, FileMode.Open))
                {
                    return await hashAlgorithm.ComputeHashAsync(fileStream);
                }
            }
        }
        private static async Task GenerateThumbnailAsync(Uri fileUrl, Uri thumbnailUrl)
        {
            // Use an image library to resize and save the thumbnail image
            using (var inputStream = File.OpenRead(fileUrl.AbsolutePath))
            using (var outputStream = File.OpenWrite(thumbnailUrl.AbsolutePath))
            {
                // Load the bitmap from the input stream
                var bitmap = SKBitmap.Decode(inputStream);

                // Calculate the new dimensions
                var width = 200;
                var height = 200;
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
                var data = image.Encode(SKEncodedImageFormat.Jpeg, 100);
                data.SaveTo(outputStream);
            }
        }

        private Task<Tuple<uint, uint>> GetImageDimensionsAsync(Uri fileUrl)
        {
            // Use an image library to get the width and height of the image
            using (var inputStream = File.OpenRead(fileUrl.AbsolutePath))
            {
                // Load the bitmap from the input stream
                var bitmap = SKBitmap.Decode(inputStream);

                // Return the dimensions as a tuple
                return Task.FromResult(new Tuple<uint, uint>((uint)bitmap.Width, (uint)bitmap.Height));
            }
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