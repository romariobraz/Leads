using LeadQualifier.Api.DTOs;
using LeadQualifier.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeadQualifier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IAgentService _agentService;
    private readonly ILeadService _leadService;

    public ChatController(IAgentService agentService, ILeadService leadService)
    {
        _agentService = agentService;
        _leadService = leadService;
    }

    /// <summary>
    /// Envia uma mensagem para o agente e recebe a resposta.
    /// </summary>
    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Message))
            return BadRequest(new { message = "A mensagem não pode ser vazia." });

        var response = await _agentService.SendMessageAsync(dto.Message);

        return Ok(new
        {
            user = dto.Message,
            assistant = response
        });
    }

    /// <summary>
    /// Usa IA para extrair informações e criar automaticamente um lead.
    /// </summary>
    [HttpPost("qualify")]
    public async Task<IActionResult> QualifyLead([FromBody] ChatMessageDto dto)
    {
        var extracted = await _agentService.ExtractLeadDataAsync(dto.Message);

        if (extracted == null)
            return BadRequest(new { message = "Não foi possível extrair dados para criar o lead." });

        var createdLead = await _leadService.CreateLeadFromAiAsync(extracted);

        return Ok(new
        {
            message = "Lead qualificado automaticamente.",
            lead = createdLead
        });
    }
}
