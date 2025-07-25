namespace BuilderStory.Contract;
public class CountWordRequestDto
{
    public string Word { get; set; } = string.Empty;
    public Guid StoryId { get; set; }
}
