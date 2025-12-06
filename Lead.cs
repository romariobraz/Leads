namespace LeadQualifier.Domain.Entities;

public class Lead
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Company { get; set; }
    public string Role { get; set; }

    public string ProblemDescription { get; set; }
    public decimal? Budget { get; set; }
    public string Urgency { get; set; }

    public int FitScore { get; set; }
    public int IntentScore { get; set; }
    public int TotalScore => FitScore + IntentScore;

    public LeadStatus Status { get; set; }
}
