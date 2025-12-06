using System.Text.RegularExpressions;

namespace LeadQualifier.Domain.ValueObjects;

public class PhoneNumber : ValueObject
{
    public string Number { get; }

    public PhoneNumber(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be empty.");

        var cleaned = Regex.Replace(number, "[^0-9]", "");

        if (cleaned.Length < 9)
            throw new ArgumentException("Invalid phone number.");

        Number = cleaned;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Number;
    }

    public override string ToString() => Number;
}
