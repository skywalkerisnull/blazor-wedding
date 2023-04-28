using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Picture
    {
        [Required]
        [Key]
        public Guid PictureId { get; set; }

        public string OriginalFileName { get; set; }
        public byte[] FileHash { get; set; }
        public DateTime DateTimeUploadedUtc { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Uri FileUrl { get; set; }
        public string? FileDescription { get; set; }
    }
}
