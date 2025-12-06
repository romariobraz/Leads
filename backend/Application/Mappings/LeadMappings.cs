using LeadQualifier.Application.DTOs.Leads;
using LeadQualifier.Application.DTOs.Common;
using LeadQualifier.Domain.Entities;
using LeadQualifier.Domain.Enums;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Application.Mappings;

public static class LeadMappings
{
    // -------------------------------
    // CreateLeadDto → Lead
    // -------------------------------
    public static Lead ToEntity(this CreateLeadDto dto)
    {
        return new Lead
        {
            Name = dto.Name,
            Email = new Email(dto.Email),
            Phone = new PhoneNumber(dto.Phone),
            Company = new CompanyName(dto.Company),
            Budget = dto.Budget,
            Need = dto.Need,
            Authority = dto.Authority,

            Status = LeadStatus.New,
            Source = dto.Source,
            Priority = dto.Priority,
            QualificationLevel = QualificationLevel.Unqualified,

            Scoring = new Scoring(0, 0)
        };
    }

    // -------------------------------
    // UpdateLeadDto → Partial Update on Lead
    // -------------------------------
    public static void ApplyUpdate(this Lead lead, UpdateLeadDto dto)
    {
        if (dto.Name != null)
            lead.Name = dto.Name;

        if (dto.Email != null)
            lead.Email = new Email(dto.Email);

        if (dto.Phone != null)
            lead.Phone = new PhoneNumber(dto.Phone);

        if (dto.Company != null)
            lead.Company = new CompanyName(dto.Company);

        if (dto.Budget.HasValue)
            lead.Budget = dto.Budget.Value;

        if (dto.Need != null)
            lead.Need = dto.Need;

        if (dto.Authority != null)
            lead.Authority = dto.Authority;

        if (dto.Status.HasValue)
            lead.Status = dto.Status.Value;

        if (dto.Source.HasValue)
            lead.Source = dto.Source.Value;

        if (dto.Priority.HasValue)
            lead.Priority = dto.Priority.Value;

        if (dto.QualificationLevel.HasValue)
            lead.QualificationLevel = dto.QualificationLevel.Value;

        lead.UpdatedAt = DateTime.UtcNow;
    }

    // -------------------------------
    // Lead → LeadResponseDto
    // -------------------------------
    public static LeadResponseDto ToResponseDto(this Lead lead)
    {
        return new LeadResponseDto(
            Id: lead.Id,
            Name: lead.Name,
            Email: lead.Email?.Address ?? "",
            Phone: lead.Phone?.Number ?? "",
            Company: lead.Company?.Name ?? "",
            Budget: lead.Budget,
            Need: lead.Need,
            Authority: lead.Authority,
            FitScore: lead.Scoring?.Fit ?? 0,
            IntentScore: lead.Scoring?.Intent ?? 0,
            Status: lead.Status,
            Source: lead.Source,
            Priority: lead.Priority,
            QualificationLevel: lead.QualificationLevel,
            CreatedAt: lead.CreatedAt,
            UpdatedAt: lead.UpdatedAt
        );
    }

    // -------------------------------
    // Lead → LeadListItemDto
    // -------------------------------
    public static LeadListItemDto ToListItemDto(this Lead lead)
    {
        return new LeadListItemDto(
            Id: lead.Id,
            Name: lead.Name,
            Email: lead.Email?.Address ?? "",
            Company: lead.Company?.Name ?? "",
            Status: lead.Status,
            Priority: lead.Priority,
            QualificationLevel: lead.QualificationLevel
        );
    }
}
