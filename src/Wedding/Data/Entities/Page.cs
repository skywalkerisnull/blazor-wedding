using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    [Index(nameof(Slug), IsUnique = true)]
    public class Page
    {
        [Required]
        [Key]
        public Guid PageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public string Slug { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public WeddingSetup? WeddingSetup { get; set; }
    }
}
