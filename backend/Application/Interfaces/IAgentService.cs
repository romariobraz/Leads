using LeadQualifier.Api.DTOs;

namespace LeadQualifier.Application.Interfaces;

public interface IAgentService
{
    Task<string> SendMessageAsync(string message);

    Task<LeadAiExtractedDto?> ExtractLeadDataAsync(string message);
}
