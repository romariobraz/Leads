using LeadQualifier.Application.DTOs.Agent;
using LeadQualifier.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LeadQualifier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AgentController : ControllerBase
{
    private readonly ILeadAgentService _agentService;

    public AgentController(ILeadAgentService agentService)
    {
        _agentService = agentService;
    }

    // --------------------------------------------
    // POST /api/agent/classify
    // Classifica lead baseado na conversa inicial
    // --------------------------------------------
    [HttpPost("classify")]
    public async Task<IActionResult> ClassifyLead([FromBody] ClassifyLeadRequestDto dto)
    {
        var result = await _agentService.ClassifyLeadAsync(dto);
        return Ok(result);
    }

    // --------------------------------------------
    // POST /api/agent/score/{leadId}
    // Score automático baseado no histórico
    // --------------------------------------------
    [HttpPost("score/{leadId}")]
    public async Task<IActionResult> ScoreLead(Guid leadId)
    {
        var result = await _agentService.ScoreLeadAsync(leadId);
        return Ok(result);
    }

    // --------------------------------------------
    // POST /api/agent/chat
    // Chatbot conversando com o lead
    // --------------------------------------------
    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] AgentChatRequestDto dto)
    {
        var response = await _agentService.ChatAsync(dto);
        return Ok(response);
    }
}
