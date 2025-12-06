using LeadQualifier.Domain.Entities;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Application.Interfaces;

public interface IScoringService
{
    Scoring ScoreLead(Lead lead);
}
