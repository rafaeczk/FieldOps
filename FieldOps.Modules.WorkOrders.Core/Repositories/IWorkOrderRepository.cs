using FieldOps.Modules.WorkOrders.Core.Entities;

namespace FieldOps.Modules.WorkOrders.Core.Repositories;

internal interface IWorkOrderRepository
{
    Task CreateAsync(WorkOrder workOrder);
    Task<WorkOrder?> GetAsync(Guid id);
    Task<IReadOnlyList<WorkOrder>> BrowseByOperatorAsync(Guid operatorId);
    Task<IReadOnlyList<WorkOrder>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<WorkOrder>> BrowseAllAsync();
    Task DeleteAsync(WorkOrder workOrder);
}
