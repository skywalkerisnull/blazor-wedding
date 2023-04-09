using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Page
    {
        [Required]
        [Key]
        public Guid PageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
