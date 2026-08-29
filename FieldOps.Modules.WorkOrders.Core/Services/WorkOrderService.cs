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
            operatorId,
            clock.UtcNow());

        await repository.CreateAsync(workOrder);
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

        return Map<WorkOrderDto>(workOrder);
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseByOperatorAsync(Guid operatorId)
    {
        var workOrders = await repository.BrowseByOperatorAsync(operatorId);
        return [.. workOrders.Select(Map<WorkOrderDto>)];
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseByTechnicianAsync(Guid technicianId)
    {
        var workOrders = await repository.BrowseByTechnicianAsync(technicianId);
        return [.. workOrders.Select(Map<WorkOrderDto>)];
    }

    public async Task<IReadOnlyList<WorkOrderDto>> BrowseAllAsync()
    {
        var workOrders = await repository.BrowseAllAsync();
        return [.. workOrders.Select(Map<WorkOrderDto>)];
    }

    public async Task UpdateStatusAsync(Guid id, UpdateWorkOrderStatusDto dto)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        try
        {
            workOrder.UpdateStatus(dto.Status, clock.UtcNow());
        }
        catch (ArgumentException)
        {
            throw new InvalidWorkOrderStatusException(dto.Status);
        }

        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new WorkOrderStatusChangedEvent(
            workOrder.Id,
            workOrder.Status,
            workOrder.TechnicianId,
            workOrder.OperatorId));
    }

    public async Task AssignTechnicianAsync(Guid id, AssignTechnicianDto dto)
    {
        var workOrder = await repository.GetAsync(id);

        if (workOrder is null)
            throw new WorkOrderNotFoundException(id);

        if (workOrder.TechnicianId is not null)
            throw new WorkOrderAlreadyAssignedException(id);

        workOrder.AssignTechnician(dto.TechnicianId, clock.UtcNow());
        await unitOfWork.SaveChangesAsync();

        await messageClient.PublishAsync(new WorkOrderStatusChangedEvent(
            workOrder.Id,
            workOrder.Status,
            workOrder.TechnicianId,
            workOrder.OperatorId));
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

    private static T Map<T>(WorkOrder workOrder) where T : WorkOrderDto, new()
        => new()
        {
            Id = workOrder.Id,
            Title = workOrder.Title,
            Description = workOrder.Description,
            Address = workOrder.Address,
            Deadline = workOrder.Deadline,
            Status = workOrder.Status,
            TechnicianId = workOrder.TechnicianId,
            OperatorId = workOrder.OperatorId,
            CreatedAt = workOrder.CreatedAt,
            UpdatedAt = workOrder.UpdatedAt
        };
}
