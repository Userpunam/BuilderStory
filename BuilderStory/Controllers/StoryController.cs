using BuilderStory.Contract;
using Microsoft.AspNetCore.Mvc;

namespace BuilderStory.Controllers;


[ApiController]
[Route("api/story")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;
    private readonly ILogger<StoryController> _logger;

    public StoryController(IStoryService storyService, ILogger<StoryController> logger)
    {
        _storyService = storyService;
        _logger = logger;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateStory([FromBody] CreateStoryRequestDto dto)
    {
        try
        {
            var story = await _storyService.CreateStoryAsync(dto.Word);
            return Ok(story);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in CreateStory for word: {dto.Word}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("")]
    public async Task<IActionResult> GetPaginatedStories([FromQuery] PaginatedStoriesRequestDto dto)
    {
        try
        {
            var stories = await _storyService.GetPaginatedStoriesAsync(dto.PageNumber, dto.PageSize);
            return Ok(stories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching paginated stories");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("count-word")]
    public async Task<IActionResult> CountWord([FromBody] CountWordRequestDto dto)
    {
        try
        {
            var result = await _storyService.CountWordInStoryAsync(dto.Word, dto.StoryText);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error counting word in story");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("{storyId}/upload-image")]
    public async Task<IActionResult> UploadImage([FromRoute] Guid storyId, IFormFile file)
    {
        try
        {
            var result = await _storyService.uploadInageAsync(storyId, file);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error uploading image for story ID: {storyId}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
