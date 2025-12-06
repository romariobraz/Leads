namespace LeadQualifier.Application.DTOs.Agent;

public record ScoreLeadResponseDto(
    int FitScore,
    int IntentScore,
    string Explanation
);
