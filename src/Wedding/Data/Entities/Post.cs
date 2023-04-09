namespace Wedding.Data.Entities
{
    public class Post : Page
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
