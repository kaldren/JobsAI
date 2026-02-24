using System.Text.RegularExpressions;

namespace JobsAI.Modules.Candidates.Domain.ValueObjects;

internal partial record Email
{
    public string Value { get; init; }

    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (!EmailRegex().IsMatch(value))
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        Value = value;
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
}
