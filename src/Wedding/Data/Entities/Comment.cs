using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Wedding.Data.Entities
{
    public class Comment
    {
        [Required]
        [Key]
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime DateCreated { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
