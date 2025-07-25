using BuilderStory.Domain;

namespace BuilderStory.Contract
{
    public interface IStoryService
    {
        Task<Story> CreateStoryAsync(string word);

        Task<PaginatedStoriesResponseDto<Story>> GetPaginatedStoriesAsync(int pageNumber, int pageSize);

        Task<CountWordResponseDto> CountWordInStoryAsync(string word, Guid storryId);

        Task<UploadImageResponseDto> uploadImageAsync(Guid storyId, IFormFile File);
    }
}
