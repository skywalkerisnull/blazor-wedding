using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Picture
    {
        [Required]
        [Key]
        public Guid PictureId { get; set; }

        public required string OriginalFileName { get; set; }
        public required byte[] FileHash { get; set; }
        public DateTime DateTimeUploadedUtc { get; set; }
        public required string FileName { get; set; }
        public required string FilePath { get; set; }
        public required Uri FileUrl { get; set; }

        public required uint FileSize { get; set; }
        public string? ThumbnailFilePath { get; set; }
        public Uri? ThumbnailUrl { get; set; }
        public uint? ThumbnailSize { get; set; }

        public uint? PixelsX { get; set; }
        public uint? PixelsY { get; set; }

        [StringLength(150)]
        public string? AlternativeText { get; set; }
        [StringLength(500)]
        public string? FileDescription { get; set; }


        public WeddingSetup? Wedding { get; set; }
    }
}

// Create a CRUD page with MudBlazor to allow for management of the images. This includes: Uploading image, downloading image, replacing image. This CRUD page should show the thumbnail, but when the image is clicked on, it should show the full resolution image, and the dialog to edit the various fields.
// Create a image selection MudComponent that allows for selecting of image to use, and uploading a new image.