namespace LeadQualifier.Domain.ValueObjects;

public class CompanyName : ValueObject
{
    public string Name { get; }

    public CompanyName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Company name cannot be empty.");

        Name = name.Trim();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString() => Name;
}
