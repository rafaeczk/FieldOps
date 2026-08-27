using FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;

public class JobPriority : ValueObject
{
    public string Value { get; }

    public const string High = "HIGH";
    public const string Medium = "MEDIUM";
    public const string Low = "LOW";
    public static readonly HashSet<string> AcceptedValues = [High, Medium, Low];

    public JobPriority(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new InvalidJobStatusException(value);

        Value = value;
    }

    public static implicit operator string(JobPriority status) => status.Value;

    public static explicit operator JobPriority(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
