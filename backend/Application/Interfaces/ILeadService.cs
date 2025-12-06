using LeadQualifier.Api.DTOs;
using LeadQualifier.Domain.Entities;

namespace LeadQualifier.Application.Interfaces;

public interface ILeadService
{
    Task<IEnumerable<LeadResponseDto>> GetAllAsync();
    Task<LeadResponseDto?> GetByIdAsync(Guid id);

    Task<LeadResponseDto> CreateLeadAsync(LeadRequestDto dto);
    Task<LeadResponseDto> CreateLeadFromAiAsync(LeadAiExtractedDto dto);

    Task<(bool Success, string Message, LeadResponseDto Lead)>
        UpdateLeadAsync(Guid id, LeadRequestDto dto);

    Task<bool> DeleteLeadAsync(Guid id);
}
