using LeadQualifier.Application.DTOs.Common;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Application.Mappings;

public static class ScoringMappings
{
    public static ScoringDto ToDto(this Scoring scoring)
        => new ScoringDto(scoring.FitScore, scoring.IntentScore);

    public static Scoring ToEntity(this ScoringDto dto)
        => new Scoring(dto.FitScore, dto.IntentScore);
}
