namespace LeadQualifier.Application.DTOs.Agent;

public record AgentChatRequestDto(
    Guid LeadId,
    string Message
);
