using LeadQualifier.Domain.Enums;

namespace LeadQualifier.Application.DTOs.Leads;

public record UpdateLeadDto(
    string? Name,
    string? Email,
    string? Phone,
    string? Company,
    decimal? Budget,
    string? Need,
    string? Authority,
    LeadStatus? Status,
    LeadSource? Source,
    LeadPriority? Priority,
    QualificationLevel? QualificationLevel
);
