namespace BuilderStory.Domain;
public class Story
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Word { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<StoryImage> Images { get; set; } = new List<StoryImage>();
}
