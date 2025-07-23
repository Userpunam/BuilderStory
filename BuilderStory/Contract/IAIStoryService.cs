namespace BuilderStory.Contract;

public interface IAIStoryService
{
    Task<string> GenerateStoryFromWordAsync(string word);
}
