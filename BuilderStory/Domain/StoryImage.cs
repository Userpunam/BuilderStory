namespace BuilderStory.Domain;
public class StoryImage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public Guid StoryId { get; set; }
    public Story Story { get; set; } = null!;
}
