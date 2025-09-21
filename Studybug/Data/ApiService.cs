using System.Text;


namespace Studybug.Data;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;
    private readonly string _apiHost;


    public ApiService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        
        _apiKey = config["RapidAPI:Key"];
        _apiHost = config["RapidAPI:ApiHost"];
        _baseUrl = config["RapidAPI:BaseUrl"];
        
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-key", _apiKey);
        _httpClient.DefaultRequestHeaders.Add("x-rapidapi-host", _apiHost);

    }

    public async Task<string> GetFlashcardsAsync(string studyNotes)
    {
        var requestUri = $"{_baseUrl}";
        
        var input = new
        {
            messages = new[]
            {
                new { role = "user", content = $"Turn these study notes into multiple flashcards: {studyNotes}. Please respond in JSON array format like this (Do NOT wrap the response in triple backticks or code blocks): [\n  {{ \"Question\": \"...\", \"Answer\": \"...\" }},\n  {{ \"Question\": \"...\", \"Answer\": \"...\" }},\n  {{ \"Question\": \"...\", \"Answer\": \"...\" }}\n]" }
            },
            web_access = false
        };
        
        var json = System.Text.Json.JsonSerializer.Serialize(input);
        var request = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PostAsync(requestUri, request);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine(jsonResponse);
        return jsonResponse;

    }
    
}