using ApiDto = LeadQualifier.Api.DTOs; // for LeadAiExtractedDto
using LeadQualifier.Application.DTOs.Leads;
using LeadQualifier.Application.Interfaces;
using LeadQualifier.Domain.Entities;
using LeadQualifier.Domain.Enums;
using LeadQualifier.Domain.ValueObjects;

namespace LeadQualifier.Application.Services;

public class LeadService : ILeadService
{
    private readonly ILeadRepository _leadRepository;
    private readonly IScoringService _scoringService;

    public LeadService(
        ILeadRepository leadRepository,
        IScoringService scoringService)
    {
        _leadRepository = leadRepository;
        _scoringService = scoringService;
    }

    public async Task<IEnumerable<LeadResponseDto>> GetAllAsync()
    {
        var leads = await _leadRepository.GetAllAsync();

        return leads.Select(MapToResponse);
    }

    public async Task<LeadResponseDto?> GetByIdAsync(Guid id)
    {
        var lead = await _leadRepository.GetByIdAsync(id);

        return lead == null ? null : MapToResponse(lead);
    }

    public async Task<LeadResponseDto> CreateLeadAsync(CreateLeadDto dto)
    {
        var lead = new Lead
        {
            Name = dto.Name,
            Email = string.IsNullOrWhiteSpace(dto.Email) ? null : new Email(dto.Email),
            Phone = null,
            Company = string.IsNullOrWhiteSpace(dto.Company) ? null : new CompanyName(dto.Company),
            Budget = dto.Budget,
            Need = dto.Need,
            Authority = dto.Authority,
            Source = dto.Source,
            Priority = dto.Priority,
            Status = LeadStatus.New
        };

        lead.Scoring = _scoringService.ScoreLead(lead);

        await _leadRepository.AddAsync(lead);
        await _leadRepository.SaveChangesAsync();

        return MapToResponse(lead);
    }

    public async Task<LeadResponseDto> CreateLeadFromAiAsync(ApiDto.LeadAiExtractedDto dto)
    {
        var lead = new Lead
        {
            Name = dto.FullName,
            Email = string.IsNullOrWhiteSpace(dto.Email) ? null : new Email(dto.Email),
            Phone = null,
            Company = string.IsNullOrWhiteSpace(dto.Company) ? null : new CompanyName(dto.Company),
            Budget = dto.Budget ?? 0m,
            Need = dto.ProblemDescription,
            Authority = null,
            Source = LeadSource.Chatbot,
            Priority = LeadPriority.High,
            Status = LeadStatus.InConversation
        };

        lead.Scoring = _scoringService.ScoreLead(lead);

        await _leadRepository.AddAsync(lead);
        await _leadRepository.SaveChangesAsync();

        return MapToResponse(lead);
    }

    public async Task<(bool Success, string Message, LeadResponseDto Lead)>
        UpdateLeadAsync(Guid id, LeadQualifier.Application.DTOs.Leads.UpdateLeadDto dto)
    {
        var lead = await _leadRepository.GetByIdAsync(id);

        if (lead == null)
            return (false, "Lead not found", null!);

        // Apply partial updates from UpdateLeadDto
        if (dto.Name != null) lead.Name = dto.Name;
        if (dto.Email != null) lead.Email = new Email(dto.Email);
        if (dto.Phone != null) lead.Phone = new PhoneNumber(dto.Phone);
        if (dto.Company != null) lead.Company = new CompanyName(dto.Company);
        if (dto.Budget.HasValue) lead.Budget = dto.Budget.Value;
        if (dto.Need != null) lead.Need = dto.Need;
        if (dto.Authority != null) lead.Authority = dto.Authority;
        if (dto.Source.HasValue) lead.Source = dto.Source.Value;
        if (dto.Priority.HasValue) lead.Priority = dto.Priority.Value;
        if (dto.Status.HasValue) lead.Status = dto.Status.Value;

        lead.Scoring = _scoringService.ScoreLead(lead);

        await _leadRepository.UpdateAsync(lead);
        await _leadRepository.SaveChangesAsync();

        return (true, "Lead updated successfully", MapToResponse(lead));
    }

    public async Task<bool> DeleteLeadAsync(Guid id)
    {
        var lead = await _leadRepository.GetByIdAsync(id);
        if (lead == null) return false;

        await _leadRepository.DeleteAsync(lead);
        await _leadRepository.SaveChangesAsync();

        return true;
    }

    private LeadResponseDto MapToResponse(Lead lead)
    {
        return new LeadResponseDto(
            Id: lead.Id,
            Name: lead.Name ?? string.Empty,
            Email: lead.Email?.Address ?? string.Empty,
            Phone: lead.Phone?.Number ?? string.Empty,
            Company: lead.Company?.Name ?? string.Empty,
            Budget: lead.Budget,
            Need: lead.Need ?? string.Empty,
            Authority: lead.Authority ?? string.Empty,
            FitScore: lead.Scoring.Fit,
            IntentScore: lead.Scoring.Intent,
            Status: lead.Status,
            Source: lead.Source,
            Priority: lead.Priority,
            QualificationLevel: lead.QualificationLevel,
            CreatedAt: lead.CreatedAt,
            UpdatedAt: lead.UpdatedAt
        );
    }
}
