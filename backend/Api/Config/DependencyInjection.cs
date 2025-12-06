using LeadQualifier.Application.Interfaces;
using LeadQualifier.Application.Services;
using LeadQualifier.Infrastructure.Persistence;
using LeadQualifier.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using OpenAI.Chat;

namespace LeadQualifier.Api.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
        );

        // Repositories
        services.AddScoped<ILeadRepository, LeadRepository>();

        // Services
        services.AddScoped<ILeadService, LeadService>();
        services.AddScoped<IScoringService, ScoringService>();
        services.AddScoped<IAgentService, AgentService>();

        // OpenAI Client
        services.AddSingleton(_ =>
        {
            var apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrWhiteSpace(apiKey))
                throw new Exception("❌ OPENAI API key não configurada.");

            return new ChatClient(
                model: "gpt-4o-mini",
                apiKey: apiKey
            );
        });

        return services;
    }
}
