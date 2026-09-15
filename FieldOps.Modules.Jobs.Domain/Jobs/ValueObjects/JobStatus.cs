using FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;

public class JobStatus : ValueObject
{
    public string Value { get; }

    public const string Pending = "PENDING";
    public const string InProgress = "IN_PROGRESS";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
    public static readonly HashSet<string> AcceptedValues = [Pending, InProgress, Completed, Cancelled];

    public JobStatus(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new InvalidJobStatusException(value);

        Value = value;
    }

    public static implicit operator string(JobStatus status) => status.Value;

    public static explicit operator JobStatus(string value) => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
