namespace FieldOps.Modules.WorkOrders.Core.DTOs;

public record WorkOrderDto
{
    public Guid Id { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
    public string Address { get; init; } = null!;
    public DateTime Deadline { get; init; }
    public string Status { get; init; } = null!;
    public Guid? TechnicianId { get; init; }
    public Guid OperatorId { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}
