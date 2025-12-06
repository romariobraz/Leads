using LeadQualifier.Domain.Enums;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Domain.Entities;

public class Lead : BaseEntity
{
    public string? Name { get; set; }

    public Email? Email { get; set; }
    public PhoneNumber? Phone { get; set; }
    public CompanyName? Company { get; set; }

    // BANT
    public decimal Budget { get; set; }
    public string? Need { get; set; }
    public string? Authority { get; set; }

    // Scoring VO
    public Scoring Scoring { get; set; } = new Scoring(0, 0);

    // Enums
    public LeadStatus Status { get; set; } = LeadStatus.New;
    public LeadSource Source { get; set; } = LeadSource.Unknown;
    public LeadPriority Priority { get; set; } = LeadPriority.Medium;
    public QualificationLevel QualificationLevel { get; set; } = QualificationLevel.Unqualified;
    
}
