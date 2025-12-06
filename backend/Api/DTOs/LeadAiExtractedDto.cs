namespace LeadQualifier.Api.DTOs;

public class LeadAiExtractedDto
{
    public string FullName { get; set; }
    public string Email { get; set; }

    public string Company { get; set; }
    public string Role { get; set; }

    public string ProblemDescription { get; set; }
    public decimal? Budget { get; set; }
    public string Urgency { get; set; }
}
