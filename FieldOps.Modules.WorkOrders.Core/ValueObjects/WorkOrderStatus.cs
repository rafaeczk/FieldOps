namespace FieldOps.Modules.WorkOrders.Core.ValueObjects;

public class WorkOrderStatus
{
    public string Value { get; }

    public const string Pending = "PENDING";
    public const string InProgress = "IN_PROGRESS";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
    public static readonly HashSet<string> AcceptedValues = [Pending, InProgress, Completed, Cancelled];

    private static readonly Dictionary<string, HashSet<string>> AllowedTransitions = new()
    {
        [Pending] = [InProgress, Cancelled],
        [InProgress] = [Completed, Cancelled],
        [Completed] = [],
        [Cancelled] = []
    };

    public static bool IsValidTransition(string from, string to)
        => AllowedTransitions.TryGetValue(from, out var allowed) && allowed.Contains(to);

    public static bool IsValid(string value)
        => AcceptedValues.Contains(value);

    public WorkOrderStatus(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new ArgumentException("The provided value is not a valid work order status.");

        Value = value;
    }

    public static implicit operator string(WorkOrderStatus status) => status.Value;

    public static explicit operator WorkOrderStatus(string value) => new(value);
}
