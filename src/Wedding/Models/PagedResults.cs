namespace Wedding.Models
{
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
