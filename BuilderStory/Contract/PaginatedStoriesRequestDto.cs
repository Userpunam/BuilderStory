namespace BuilderStory.Contract;
public class PaginatedStoriesRequestDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
