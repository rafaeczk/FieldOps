namespace FieldOps.Modules.Reports.Core.ValueObjects;

public class SyncStatus
{
    public string Value { get; }

    public const string Pending = "PENDING";
    public const string Synced = "SYNCED";
    public const string Conflict = "CONFLICT";
    public static readonly HashSet<string> AcceptedValues = [Pending, Synced, Conflict];

    public SyncStatus(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new ArgumentException("The provided value is not a valid sync status.");

        Value = value;
    }

    public static implicit operator string(SyncStatus status) => status.Value;

    public static explicit operator SyncStatus(string value) => new(value);
}
