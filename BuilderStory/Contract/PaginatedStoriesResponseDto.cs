namespace BuilderStory.Contract;
public class PaginatedStoriesResponseDto<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
