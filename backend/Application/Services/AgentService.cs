using LeadQualifier.Api.DTOs;
using LeadQualifier.Application.Interfaces;
using System.Text.Json;

namespace LeadQualifier.Application.Services;

public class AgentService : IAgentService
{
    private readonly HttpClient _http;

    public AgentService(IHttpClientFactory httpFactory)
    {
        _http = httpFactory.CreateClient("OpenAI");
    }

    public async Task<string> SendMessageAsync(string message)
    {
        var payload = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "Você é um agente de qualificação de leads." },
                new { role = "user", content = message }
            }
        };

        var response = await _http.PostAsJsonAsync("v1/chat/completions", payload);
        var content = await response.Content.ReadAsStringAsync();

        return JsonDocument.Parse(content)
            .RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()!;
    }

    public async Task<LeadAiExtractedDto?> ExtractLeadDataAsync(string message)
    {
        var payload = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "Extraia dados estruturados de lead em JSON." },
                new { role = "user", content = message }
            },
            response_format = new { type = "json_object" }
        };

        var res = await _http.PostAsJsonAsync("v1/chat/completions", payload);
        var json = await res.Content.ReadAsStringAsync();

        try
        {
            var doc = JsonDocument.Parse(json)
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return JsonSerializer.Deserialize<LeadAiExtractedDto>(doc!);
        }
        catch
        {
            return null;
        }
    }
}
