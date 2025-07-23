using AutoMapper;
using BuilderStory.Contract;
using BuilderStory.Data;
using BuilderStory.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BuilderStory.Services;

public class StoryService : IStoryService
{
    private readonly StoryDbContext _context;
    private readonly IMapper _mapper;
    private readonly AIStoryService _aiStoryService;
    private readonly ILogger<StoryService> _logger;

    public StoryService(StoryDbContext context, IMapper mapper, AIStoryService aiStoryService, ILogger<StoryService> logger)
    {
        _context = context;
        _mapper = mapper;
        _aiStoryService = aiStoryService;
        _logger = logger;
    }

    public async Task<Story> CreateStoryAsync(string word)
    {
        try
        {
            var existing = await _context.Stories.FirstOrDefaultAsync(x => x.Word == word);
            if (existing != null)
            {
                _logger.LogInformation("Story already exists for word: {Word}", word);
                return existing;
            }

            var response = await _aiStoryService.GenerateStoryFromWordAsync(word);

            var story = new Story
            {
                Word = word,
                Content = response,
                CreatedAt = DateTime.UtcNow
            };

            _context.Stories.Add(story);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Story created successfully for word: {Word}", word);
            return story;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating story for word: {Word}", word);
            throw;
        }
    }

    public async Task<PaginatedStoriesResponseDto<Story>> GetPaginatedStoriesAsync(int pageNumber, int pageSize)
    {
        try
        {
            var query = _context.Stories.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            _logger.LogInformation("Retrieved {Count} stories on page {PageNumber}", items.Count, pageNumber);

            return new PaginatedStoriesResponseDto<Story>
            {
                Items = items,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while fetching paginated stories");
            throw;
        }
    }

    public async Task<CountWordResponseDto> CountWordInStoryAsync(string word, string storyText)
    {
        try
        {
            int count = storyText.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                                 .Count(w => w.Equals(word, StringComparison.OrdinalIgnoreCase));

            _logger.LogInformation("Counted {Count} occurrences of word '{Word}'", count, word);

            return new CountWordResponseDto
            {
                Word = word,
                StoryText = storyText
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while counting word '{Word}' in story text", word);
            throw;
        }
    }

    public async Task<UploadImageResponseDto> uploadInageAsync(Guid storyId, IFormFile file)
    {
        try
        {
            var story = await _context.Stories.Include(s => s.Images).FirstOrDefaultAsync(s => s.Id == storyId);
            if (story == null)
            {
                _logger.LogWarning("Story not found for ID: {StoryId}", storyId);
                throw new Exception("Story not found");
            }

            var storyImage = new StoryImage
            {
                StoryId = storyId,
                FileName = file.FileName,
                ContentType = file.ContentType,
                FileSize = file.Length,
                UploadedAt = DateTime.UtcNow
            };

            story.Images.Add(storyImage);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Image uploaded for Story ID: {StoryId}, File: {FileName}", storyId, file.FileName);

            return new UploadImageResponseDto
            {
                Storyid = storyId,
                FileName = file.FileName,
                FileSize = file.Length,
                ContentType = file.ContentType,
                UploadAt = storyImage.UploadedAt
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while uploading image for story ID: {StoryId}", storyId);
            throw;
        }
    }
}
