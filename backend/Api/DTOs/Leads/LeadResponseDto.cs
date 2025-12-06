using LeadQualifier.Domain.Enums;

namespace LeadQualifier.Application.DTOs.Leads;

public record LeadResponseDto(
    Guid Id,
    string Name,
    string Email,
    string Phone,
    string Company,
    decimal Budget,
    string Need,
    string Authority,
    int FitScore,
    int IntentScore,
    LeadStatus Status,
    LeadSource Source,
    LeadPriority Priority,
    QualificationLevel QualificationLevel,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
