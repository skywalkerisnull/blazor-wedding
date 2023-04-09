using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Category
    {
        [Required]
        [Key]
        public Guid CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
