using LeadQualifier.Application.DTOs.Leads;
using LeadQualifier.Application.Interfaces;
using LeadQualifier.Application.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace LeadQualifier.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
    private readonly ILeadService _leadService;

    public LeadsController(ILeadService leadService)
    {
        _leadService = leadService;
    }

    // -------------------------------------------------------
    // GET: api/leads
    // -------------------------------------------------------
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var leads = await _leadService.GetAllAsync();
        return Ok(leads.Select(l => l.ToListItemDto()));
    }

    // -------------------------------------------------------
    // GET: api/leads/{id}
    // -------------------------------------------------------
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var lead = await _leadService.GetByIdAsync(id);
        if (lead is null)
            return NotFound();

        return Ok(lead.ToResponseDto());
    }

    // -------------------------------------------------------
    // POST: api/leads
    // -------------------------------------------------------
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLeadDto dto)
    {
        var result = await _leadService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result.ToResponseDto());
    }

    // -------------------------------------------------------
    // PUT: api/leads/{id}
    // (update completo)
    // -------------------------------------------------------
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLeadDto dto)
    {
        var updatedLead = await _leadService.UpdateAsync(id, dto);
        if (updatedLead is null)
            return NotFound();

        return Ok(updatedLead.ToResponseDto());
    }

    // -------------------------------------------------------
    // PATCH: api/leads/{id}
    // (parcial)
    // -------------------------------------------------------
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartialUpdate(Guid id, [FromBody] UpdateLeadDto dto)
    {
        var updatedLead = await _leadService.UpdateAsync(id, dto);
        if (updatedLead is null)
            return NotFound();

        return Ok(updatedLead.ToResponseDto());
    }

    // -------------------------------------------------------
    // DELETE: api/leads/{id}
    // -------------------------------------------------------
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _leadService.DeleteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
