namespace FieldOps.Modules.WorkOrders.Core.ValueObjects;

public class WorkOrderPriority
{
    public string Value { get; }

    public const string Low = "LOW";
    public const string Medium = "MEDIUM";
    public const string High = "HIGH";
    public static readonly HashSet<string> AcceptedValues = [Low, Medium, High];

    public WorkOrderPriority(string value)
    {
        if (!AcceptedValues.Contains(value))
            throw new ArgumentException("The provided value is not a valid work order priority.");

        Value = value;
    }

    public static bool IsValid(string value)
        => AcceptedValues.Contains(value);

    public static implicit operator string(WorkOrderPriority priority) => priority.Value;

    public static explicit operator WorkOrderPriority(string value) => new(value);
}
