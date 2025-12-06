namespace LeadQualifier.Application.DTOs.Agent;

public record ClassifyLeadResponseDto(
    string Summary,
    bool IsQualified,
    string Reason,
    int FitScore,
    int IntentScore
);
