using System.ComponentModel.DataAnnotations;

namespace LeadQualifier.Api.DTOs;

public class LeadRequestDto
{
    [Required]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Company { get; set; }

    public string Role { get; set; }

    public string ProblemDescription { get; set; }

    public decimal? Budget { get; set; }

    public string Urgency { get; set; }
}
