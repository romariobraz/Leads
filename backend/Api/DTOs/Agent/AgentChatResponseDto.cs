namespace LeadQualifier.Application.DTOs.Agent;

public record AgentChatResponseDto(
    string Response,
    bool ShouldEscalateToHuman
);
