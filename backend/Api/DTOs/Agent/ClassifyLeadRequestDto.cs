namespace LeadQualifier.Application.DTOs.Agent;

public record ClassifyLeadRequestDto(
    string Name,
    string Email,
    string Phone,
    string Company,
    string ConversationText
);
