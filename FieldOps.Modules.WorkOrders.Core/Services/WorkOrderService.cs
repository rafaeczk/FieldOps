using FieldOps.Modules.WorkOrders.Core.DTOs;
using FieldOps.Modules.WorkOrders.Core.Entities;
using FieldOps.Modules.WorkOrders.Core.Events;
using FieldOps.Modules.WorkOrders.Core.Exceptions;
using FieldOps.Modules.WorkOrders.Core.Repositories;
using FieldOps.Modules.WorkOrders.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.WorkOrders.Core.Services;

internal class WorkOrderService(
    IWorkOrderRepository repository,
    IWorkOrderUnitOfWork unitOfWork,
    IMessageClient messageClient,
    IClock clock) : IWorkOrderService
{
    private readonly IWorkOrderRepository repository = repository;
    private readonly IWorkOrderUnitOfWork unitOfWork = unitOfWork;
    private readonly IMessageClient messageClient = messageClient;
    private readonly IClock clock = clock;

    public async Task<Guid> CreateAsync(CreateWorkOrderDto dto, Guid operatorId)
    {
        var workOrder = WorkOrder.Create(
            dto.Title,
            dto.Description,
            dto.Address,
            dto.Deadline,
            dto.Priority,
            operatorId,
            clock.UtcNow());

        await repository.CreateAsync(workOrder);

        if (dto.TechnicianIds is { Count: > 0 })
        {
            foreach (var technicianId in dto.TechnicianIds)
            {
                await repository.AddAssigneeAsync(workOrder.Id, technicianId, clock.UtcNow());
            }
        }

        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new WorkOrderCreatedEvent(
            workOrder.Id,
            workOrder.Title,
            workOrder.Address,
            workOrder.Deadline,
            workOrder.OperatorId));

        return workOrder.Id;
    }

    public async Task<WorkOrderDto?> GetByAsync(Guid id)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            return null;

        var assignees = await repository.GetAssigneesAsync(id);
        return Map(workOrder, assignees);
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseByOperatorAsync(Guid operatorId)
    {
        var workOrders = await repository.BrowseByOperatorAsync(operatorId);
        return await MapRange(workOrders);
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseByTechnicianAsync(Guid technicianId)
    {
        var workOrders = await repository.BrowseByTechnicianAsync(technicianId);
        return await MapRange(workOrders);
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseAllAsync()
    {
        var workOrders = await repository.BrowseAllAsync();
        return await MapRange(workOrders);
    }

    public async Task<WorkOrderListDto> BrowseByOperatorAsync(Guid operatorId, WorkOrderFilterDto filter)
    {
        var (items, totalCount) = await repository.BrowseByOperatorAsync(operatorId, filter);
        var dtos = await MapRange(items);
        return new WorkOrderListDto(dtos, totalCount, filter.Page, filter.PageSize);
    }

    public async Task<WorkOrderListDto> BrowseByTechnicianAsync(Guid technicianId, WorkOrderFilterDto filter)
    {
        var (items, totalCount) = await repository.BrowseByTechnicianAsync(technicianId, filter);
        var dtos = await MapRange(items);
        return new WorkOrderListDto(dtos, totalCount, filter.Page, filter.PageSize);
    }

    public async Task<WorkOrderListDto> BrowseAllAsync(WorkOrderFilterDto filter)
    {
        var (items, totalCount) = await repository.BrowseAllAsync(filter);
        var dtos = await MapRange(items);
        return new WorkOrderListDto(dtos, totalCount, filter.Page, filter.PageSize);
    }

    public async Task UpdateAsync(Guid id, UpdateWorkOrderDto dto)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        if (workOrder.Status == WorkOrderStatus.Completed || workOrder.Status == WorkOrderStatus.Cancelled)
            throw new WorkOrderImmutableException(workOrder.Id, workOrder.Status, "edit");

        workOrder.UpdateDetails(dto.Title, dto.Description, dto.Address, dto.Deadline, dto.Priority, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateStatusAsync(Guid id, UpdateWorkOrderStatusDto dto)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        if (!WorkOrderStatus.IsValid(dto.Status))
            throw new InvalidWorkOrderStatusException(dto.Status);

        workOrder.UpdateStatus(dto.Status, clock.UtcNow());

        await unitOfWork.SaveChangesAsync();

        var assignees = await repository.GetAssigneesAsync(id);
        await messageClient.PublishAsync(new WorkOrderStatusChangedEvent(
            workOrder.Id,
            workOrder.Status,
            assignees.Select(a => a.TechnicianId).ToList(),
            workOrder.OperatorId));
    }

    public async Task AssignTechnicianAsync(Guid id, AssignTechnicianDto dto)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        if (await repository.IsAssignedAsync(id, dto.TechnicianId))
            return;

        if (workOrder.Status == WorkOrderStatus.Completed || workOrder.Status == WorkOrderStatus.Cancelled)
            throw new WorkOrderImmutableException(workOrder.Id, workOrder.Status, "assign technicians to");

        if (workOrder.Status == WorkOrderStatus.Pending)
            workOrder.UpdateStatus(WorkOrderStatus.InProgress, clock.UtcNow());

        await repository.AddAssigneeAsync(id, dto.TechnicianId, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();

        var assignees = await repository.GetAssigneesAsync(id);
        await messageClient.PublishAsync(new WorkOrderStatusChangedEvent(
            workOrder.Id,
            workOrder.Status,
            assignees.Select(a => a.TechnicianId).ToList(),
            workOrder.OperatorId));
    }

    public async Task UnassignTechnicianAsync(Guid workOrderId, Guid technicianId)
    {
        var workOrder = await repository.GetAsync(workOrderId);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(workOrderId);

        if (workOrder.Status == WorkOrderStatus.Completed || workOrder.Status == WorkOrderStatus.Cancelled)
            throw new WorkOrderImmutableException(workOrder.Id, workOrder.Status, "unassign technicians from");

        await repository.RemoveAssigneeAsync(workOrderId, technicianId);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        if (await repository.HasReportsAsync(id))
            throw new WorkOrderHasReportsException(id);

        await repository.DeleteAsync(workOrder);
        await unitOfWork.SaveChangesAsync();
    }

    private async Task<IReadOnlyList<WorkOrderDto>> MapRange(IReadOnlyList<WorkOrder> workOrders)
    {
        var orderIds = workOrders.Select(wo => wo.Id).ToList();
        var allAssignees = await repository.GetAssigneesForOrdersAsync(orderIds);
        var assigneesByOrder = allAssignees
            .GroupBy(a => a.WorkOrderId)
            .ToDictionary(g => g.Key, g => (IReadOnlyList<WorkOrderAssignee>)g.ToList());

        return workOrders.Select(wo =>
        {
            assigneesByOrder.TryGetValue(wo.Id, out var assignees);
            return Map(wo, assignees ?? []);
        }).ToList();
    }

    private static WorkOrderDto Map(WorkOrder workOrder, IReadOnlyList<WorkOrderAssignee> assignees)
        => new()
        {
            Id = workOrder.Id,
            Title = workOrder.Title,
            Description = workOrder.Description,
            Address = workOrder.Address,
            Deadline = workOrder.Deadline,
            Status = workOrder.Status,
            Priority = workOrder.Priority,
            TechnicianIds = assignees.Select(a => a.TechnicianId).ToList(),
            OperatorId = workOrder.OperatorId,
            CreatedAt = workOrder.CreatedAt,
            UpdatedAt = workOrder.UpdatedAt
        };
}
