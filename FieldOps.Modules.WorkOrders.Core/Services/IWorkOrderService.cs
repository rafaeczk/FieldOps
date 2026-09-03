using FieldOps.Modules.WorkOrders.Core.DTOs;

namespace FieldOps.Modules.WorkOrders.Core.Services;

public interface IWorkOrderService
{
    Task<Guid> CreateAsync(CreateWorkOrderDto dto, Guid operatorId);
    Task<WorkOrderDto?> GetByAsync(Guid id);
    Task<IReadOnlyList<WorkOrderDto>> BrowseByOperatorAsync(Guid operatorId);
    Task<IReadOnlyList<WorkOrderDto>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<WorkOrderDto>> BrowseAllAsync();
    Task<WorkOrderListDto> BrowseByOperatorAsync(Guid operatorId, WorkOrderFilterDto filter);
    Task<WorkOrderListDto> BrowseByTechnicianAsync(Guid technicianId, WorkOrderFilterDto filter);
    Task<WorkOrderListDto> BrowseAllAsync(WorkOrderFilterDto filter);
    Task UpdateAsync(Guid id, UpdateWorkOrderDto dto);
    Task UpdateStatusAsync(Guid id, UpdateWorkOrderStatusDto dto);
    Task AssignTechnicianAsync(Guid id, AssignTechnicianDto dto);
    Task UnassignTechnicianAsync(Guid workOrderId, Guid technicianId);
    Task DeleteAsync(Guid id);
}
