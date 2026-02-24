using System.Text.RegularExpressions;

namespace JobsAI.Modules.Candidates.Domain.ValueObjects;

internal partial record Phone
{
    public string Value { get; init; }

    public Phone(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (!PhoneRegex().IsMatch(value))
        {
            throw new ArgumentException("Invalid phone number format.", nameof(value));
        }

        Value = value;
    }

    [GeneratedRegex(@"^\+?[\d\s\-()]{7,20}$")]
    private static partial Regex PhoneRegex();
}
