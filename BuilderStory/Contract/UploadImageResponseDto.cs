namespace BuilderStory.Contract;
public class UploadImageResponseDto
{
    public Guid Storyid { get; set; }
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public DateTime UploadAt { get; set; } = DateTime.UtcNow;
}
