using System.Text.RegularExpressions;

namespace LeadQualifier.Domain.ValueObjects;

public class Email : ValueObject
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("E-mail cannot be empty.");

        if (!Regex.IsMatch(address, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            throw new ArgumentException("Invalid e-mail format.");

        Address = address.Trim().ToLower();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
    }

    public override string ToString() => Address;
}
