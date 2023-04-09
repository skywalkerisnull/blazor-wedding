using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Tag
    {
        [Required]
        [Key]
        public Guid TagId { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
