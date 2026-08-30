namespace FieldOps.Modules.WorkOrders.Core.DTOs;

public record CreateWorkOrderDto(
    string Title,
    string? Description,
    string Address,
    DateTime Deadline,
    string Priority = "MEDIUM",
    List<Guid>? TechnicianIds = null);
