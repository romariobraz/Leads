using LeadQualifier.Domain.Enums;

namespace LeadQualifier.Application.DTOs.Leads;

public record CreateLeadDto(
    string Name,
    string Email,
    string Phone,
    string Company,
    decimal Budget,
    string Need,
    string Authority,
    LeadSource Source = LeadSource.Unknown,
    LeadPriority Priority = LeadPriority.Medium
);
