using FieldOps.Modules.WorkOrders.Core.DTOs;
using FieldOps.Modules.WorkOrders.Core.Entities;

namespace FieldOps.Modules.WorkOrders.Core.Repositories;

internal interface IWorkOrderRepository
{
    Task CreateAsync(WorkOrder workOrder);
    Task<WorkOrder?> GetAsync(Guid id);
    Task<IReadOnlyList<WorkOrder>> BrowseByOperatorAsync(Guid operatorId);
    Task<IReadOnlyList<WorkOrder>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<WorkOrder>> BrowseAllAsync();
    Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseByOperatorAsync(Guid operatorId, WorkOrderFilterDto filter);
    Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseByTechnicianAsync(Guid technicianId, WorkOrderFilterDto filter);
    Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseAllAsync(WorkOrderFilterDto filter);
    Task<bool> HasReportsAsync(Guid workOrderId);
    Task DeleteAsync(WorkOrder workOrder);
    Task<IReadOnlyList<WorkOrderAssignee>> GetAssigneesAsync(Guid workOrderId);
    Task<IReadOnlyList<WorkOrderAssignee>> GetAssigneesForOrdersAsync(IEnumerable<Guid> workOrderIds);
    Task AddAssigneeAsync(Guid workOrderId, Guid technicianId, DateTime assignedAt);
    Task RemoveAssigneeAsync(Guid workOrderId, Guid technicianId);
    Task<bool> IsAssignedAsync(Guid workOrderId, Guid technicianId);
}
