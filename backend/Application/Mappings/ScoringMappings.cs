using LeadQualifier.Application.DTOs.Common;
using LeadQualifier.Domain.ValueObjects;
using LeadQualifier.Application.DTOs.Common;

namespace LeadQualifier.Application.Mappings;

public static class ScoringMappings
{
    public static ScoringDto ToDto(this Scoring scoring)
        => new ScoringDto(scoring.Fit, scoring.Intent);

    public static Scoring ToEntity(this ScoringDto dto)
        => new Scoring(dto.FitScore, dto.IntentScore);
}
