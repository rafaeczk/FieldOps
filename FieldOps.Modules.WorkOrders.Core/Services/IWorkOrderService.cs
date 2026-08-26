using FieldOps.Modules.WorkOrders.Core.DTOs;

namespace FieldOps.Modules.WorkOrders.Core.Services;

public interface IWorkOrderService
{
    Task<Guid> CreateAsync(CreateWorkOrderDto dto, Guid operatorId);
    Task<WorkOrderDto?> GetByAsync(Guid id);
    Task<IReadOnlyList<WorkOrderDto>> BrowseByOperatorAsync(Guid operatorId);
    Task<IReadOnlyList<WorkOrderDto>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<WorkOrderDto>> BrowseAllAsync();
    Task UpdateStatusAsync(Guid id, UpdateWorkOrderStatusDto dto);
    Task AssignTechnicianAsync(Guid id, AssignTechnicianDto dto);
    Task DeleteAsync(Guid id);
}
