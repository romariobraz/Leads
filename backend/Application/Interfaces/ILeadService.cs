using LeadQualifier.Application.DTOs.Leads;
using LeadQualifier.Domain.Entities;
using ApiDto = LeadQualifier.Api.DTOs;

namespace LeadQualifier.Application.Interfaces;

public interface ILeadService
{
    Task<IEnumerable<LeadResponseDto>> GetAllAsync();
    Task<LeadResponseDto?> GetByIdAsync(Guid id);

    Task<LeadResponseDto> CreateLeadAsync(CreateLeadDto dto);
    Task<LeadResponseDto> CreateLeadFromAiAsync(ApiDto.LeadAiExtractedDto dto);

    Task<(bool Success, string Message, LeadResponseDto Lead)>
        UpdateLeadAsync(Guid id, LeadQualifier.Application.DTOs.Leads.UpdateLeadDto dto);

    Task<bool> DeleteLeadAsync(Guid id);
}
