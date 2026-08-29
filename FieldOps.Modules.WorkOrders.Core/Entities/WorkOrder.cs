using FieldOps.Modules.WorkOrders.Core.ValueObjects;

namespace FieldOps.Modules.WorkOrders.Core.Entities;

internal class WorkOrder
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string Address { get; private set; } = null!;
    public DateTime Deadline { get; private set; }
    public WorkOrderStatus Status { get; private set; } = null!;
    public WorkOrderPriority Priority { get; private set; } = null!;
    public Guid OperatorId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private WorkOrder() { }

    public static WorkOrder Create(
        string title,
        string? description,
        string address,
        DateTime deadline,
        string priority,
        Guid operatorId,
        DateTime createdAt)
    {
        return new WorkOrder
        {
            Id = Guid.NewGuid(),
            Title = title,
            Description = description,
            Address = address,
            Deadline = deadline,
            Status = new WorkOrderStatus(WorkOrderStatus.Pending),
            Priority = new WorkOrderPriority(priority),
            OperatorId = operatorId,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public static WorkOrder Create(
        Guid id,
        string title,
        string? description,
        string address,
        DateTime deadline,
        WorkOrderStatus status,
        WorkOrderPriority priority,
        Guid operatorId,
        DateTime createdAt)
    {
        return new WorkOrder
        {
            Id = id,
            Title = title,
            Description = description,
            Address = address,
            Deadline = deadline,
            Status = status,
            Priority = priority,
            OperatorId = operatorId,
            CreatedAt = createdAt,
            UpdatedAt = createdAt
        };
    }

    public void UpdateStatus(string status, DateTime updatedAt)
    {
        if (!WorkOrderStatus.IsValidTransition(Status, status))
            throw new Exceptions.InvalidWorkOrderTransitionException(Status, status);

        Status = new WorkOrderStatus(status);
        UpdatedAt = updatedAt;
    }

    public void UpdateDetails(string title, string? description, string address, DateTime deadline, string priority, DateTime updatedAt)
    {
        Title = title;
        Description = description;
        Address = address;
        Deadline = deadline;
        Priority = new WorkOrderPriority(priority);
        UpdatedAt = updatedAt;
    }
}
