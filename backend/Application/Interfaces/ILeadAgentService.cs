using LeadQualifier.Application.DTOs.Agent;

namespace LeadQualifier.Application.Interfaces;

public interface ILeadAgentService
{
    Task<ClassifyLeadResponseDto> ClassifyLeadAsync(ClassifyLeadRequestDto dto);
    Task<ScoreLeadResponseDto> ScoreLeadAsync(Guid leadId);
    Task<AgentChatResponseDto> ChatAsync(AgentChatRequestDto dto);
}
