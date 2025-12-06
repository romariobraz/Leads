using LeadQualifier.Application.Interfaces;
using LeadQualifier.Domain.Entities;
using LeadQualifier.Domain.Enums;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Application.Services;

public class ScoringService : IScoringService
{
    public Scoring ScoreLead(Lead lead)
    {
        int fit = 0;

        if (lead.Company != null) fit += 25;
        if (lead.Budget > 0) fit += 25;
        if (!string.IsNullOrWhiteSpace(lead.Authority)) fit += 25;
        if (!string.IsNullOrWhiteSpace(lead.Need)) fit += 25;

        int intent = Random.Shared.Next(20, 100);

        // Atualiza QualificationLevel
        lead.QualificationLevel = fit switch
        {
            <= 25 => QualificationLevel.Weak,
            <= 50 => QualificationLevel.Moderate,
            <= 75 => QualificationLevel.Strong,
            _ => QualificationLevel.PerfectFit
        };

        return new Scoring(fit, intent);
    }
}
