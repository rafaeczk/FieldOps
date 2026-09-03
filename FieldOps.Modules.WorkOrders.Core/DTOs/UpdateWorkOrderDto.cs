namespace FieldOps.Modules.WorkOrders.Core.DTOs;

public record UpdateWorkOrderDto(
    string Title,
    string? Description,
    string Address,
    DateTime Deadline,
    string Priority);
