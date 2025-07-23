using System.Text.Json;
using System.Text;

namespace BuilderStory.Services;
public class AIStoryService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AIStoryService> _logger;

    public AIStoryService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AIStoryService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string> GenerateStoryFromWordAsync(string word)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient();

            var requestBody = new
            {
                model = "openai/gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = $"Write a short story using the word: {word}" }
                }
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://openrouter.ai/api/v1/chat/completions")
            {
                Headers =
                {
                    { "Authorization", $"Bearer {_configuration["OpenRouter:ApiKey"]}" },
                    { "HTTP-Referer", "https://yourproject.com" },
                    { "X-Title", "Word Story Builder Project" }
                },
                Content = new StringContent(jsonRequest, Encoding.UTF8, "application/json")
            };

            var response = await httpClient.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(jsonResponse);
            var story = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return story?.Trim() ?? "No story generated";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"AI API failed while generating story for word: {word}");
            return $"'{word}' Unable to generate story for '{word}' due to an error.";
        }
    }
}
