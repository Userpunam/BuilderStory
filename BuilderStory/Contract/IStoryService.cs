using BuilderStory.Domain;

namespace BuilderStory.Contract
{
    public interface IStoryService
    {
        Task<Story> CreateStoryAsync(string word);

        Task<PaginatedStoriesResponseDto<Story>> GetPaginatedStoriesAsync(int pageNumber, int pageSize);

        Task<CountWordResponseDto> CountWordInStoryAsync(string word, string storyText);

        Task<UploadImageResponseDto> uploadInageAsync(Guid storyId, IFormFile File);
    }
}
