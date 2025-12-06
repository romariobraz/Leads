using LeadQualifier.Domain.Enums;

namespace LeadQualifier.Application.DTOs.Leads;

public record LeadListItemDto(
    Guid Id,
    string Name,
    string Email,
    string Company,
    LeadStatus Status,
    LeadPriority Priority,
    QualificationLevel QualificationLevel
);
