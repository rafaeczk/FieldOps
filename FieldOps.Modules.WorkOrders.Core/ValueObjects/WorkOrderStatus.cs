namespace FieldOps.Modules.WorkOrders.Core.ValueObjects;

public class WorkOrderStatus
{
    public string Value { get; }

    public const string Pending = "PENDING";
    public const string InProgress = "IN_PROGRESS";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
    public static readonly HashSet<string> AcceptedValues = [Pending, InProgress, Completed, Cancelled];

    public WorkOrderStatus(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new ArgumentException("The provided value is not a valid work order status.");

        Value = value;
    }

    public static implicit operator string(WorkOrderStatus status) => status.Value;

    public static explicit operator WorkOrderStatus(string value) => new(value);
}
