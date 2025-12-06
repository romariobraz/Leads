using LeadQualifier.Application.Interfaces;
using LeadQualifier.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace LeadQualifier.Api.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Application services
        services.AddScoped<ILeadService, LeadService>();
        services.AddScoped<IScoringService, ScoringService>();
        services.AddScoped<IAgentService, AgentService>();

        // Configure named HttpClient for OpenAI usage in AgentService
        var apiKey = configuration["OpenAI:ApiKey"];
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new Exception("❌ OPENAI API key não configurada.");

        services.AddHttpClient("OpenAI", client =>
        {
            client.BaseAddress = new Uri("https://api.openai.com/");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}
