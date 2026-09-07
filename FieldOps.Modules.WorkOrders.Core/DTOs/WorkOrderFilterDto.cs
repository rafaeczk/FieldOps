namespace FieldOps.Modules.WorkOrders.Core.DTOs;

public record WorkOrderFilterDto(
    string? Status = null,
    string? Priority = null,
    DateTime? DateFrom = null,
    DateTime? DateTo = null,
    int Page = 1,
    int PageSize = 20);

public record WorkOrderListDto(
    IReadOnlyList<WorkOrderDto> Items,
    int TotalCount,
    int Page,
    int PageSize);
