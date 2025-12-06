using LeadQualifier.Domain.Entities;

namespace LeadQualifier.Application.Interfaces;

public interface IScoringService
{
    (int fit, int intent) ScoreLead(Lead lead);
}
