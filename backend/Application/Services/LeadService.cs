using LeadQualifier.Api.DTOs;
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

    public async Task<LeadResponseDto> CreateLeadAsync(LeadRequestDto dto)
    {
        var lead = new Lead
        {
            Name = dto.Name,
            Email = string.IsNullOrWhiteSpace(dto.Email) ? null : new Email(dto.Email),
            Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : new PhoneNumber(dto.Phone),
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

    public async Task<LeadResponseDto> CreateLeadFromAiAsync(LeadAiExtractedDto dto)
    {
        var lead = new Lead
        {
            Name = dto.Name,
            Email = string.IsNullOrWhiteSpace(dto.Email) ? null : new Email(dto.Email),
            Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : new PhoneNumber(dto.Phone),
            Company = string.IsNullOrWhiteSpace(dto.Company) ? null : new CompanyName(dto.Company),
            Budget = dto.Budget,
            Need = dto.Need,
            Authority = dto.Authority,
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
        UpdateLeadAsync(Guid id, LeadRequestDto dto)
    {
        var lead = await _leadRepository.GetByIdAsync(id);

        if (lead == null)
            return (false, "Lead not found", null!);

        lead.Name = dto.Name;
        lead.Email = string.IsNullOrWhiteSpace(dto.Email) ? null : new Email(dto.Email);
        lead.Phone = string.IsNullOrWhiteSpace(dto.Phone) ? null : new PhoneNumber(dto.Phone);
        lead.Company = string.IsNullOrWhiteSpace(dto.Company) ? null : new CompanyName(dto.Company);

        lead.Budget = dto.Budget;
        lead.Need = dto.Need;
        lead.Authority = dto.Authority;

        lead.Source = dto.Source;
        lead.Priority = dto.Priority;
        lead.Status = dto.Status;

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
        return new LeadResponseDto
        {
            Id = lead.Id,
            Name = lead.Name,
            Email = lead.Email?.Address,
            Phone = lead.Phone?.Number,
            Company = lead.Company?.Name,
            Budget = lead.Budget,
            Need = lead.Need,
            Authority = lead.Authority,
            FitScore = lead.Scoring.Fit,
            IntentScore = lead.Scoring.Intent,
            Source = lead.Source,
            Status = lead.Status,
            Priority = lead.Priority,
            QualificationLevel = lead.QualificationLevel,
            CreatedAt = lead.CreatedAt
        };
    }
}
