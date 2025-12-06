namespace LeadQualifier.Domain.ValueObjects;

public class Scoring : ValueObject
{
    public int Fit { get; }
    public int Intent { get; }

    public Scoring(int fit, int intent)
    {
        if (fit < 0 || fit > 100)
            throw new ArgumentException("Fit score must be between 0 and 100.");

        if (intent < 0 || intent > 100)
            throw new ArgumentException("Intent score must be between 0 and 100.");

        Fit = fit;
        Intent = intent;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Fit;
        yield return Intent;
    }

    public override string ToString() => $"Fit: {Fit}, Intent: {Intent}";
}
