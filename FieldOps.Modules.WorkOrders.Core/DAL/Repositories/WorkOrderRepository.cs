using FieldOps.Modules.WorkOrders.Core.DTOs;
using FieldOps.Modules.WorkOrders.Core.Entities;
using FieldOps.Modules.WorkOrders.Core.Repositories;
using FieldOps.Modules.WorkOrders.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.WorkOrders.Core.DAL.Repositories;

internal class WorkOrderRepository(WorkOrderDbContext context) : IWorkOrderRepository
{
    private readonly WorkOrderDbContext context = context;

    public Task CreateAsync(WorkOrder workOrder)
    {
        context.WorkOrders.Add(workOrder);
        return Task.CompletedTask;
    }

    public async Task<WorkOrder?> GetAsync(Guid id)
    {
        return await context.WorkOrders.SingleOrDefaultAsync(wo => wo.Id == id);
    }

    public async Task<IReadOnlyList<WorkOrder>> BrowseByOperatorAsync(Guid operatorId)
    {
        return await context.WorkOrders
            .Where(wo => wo.OperatorId == operatorId)
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<WorkOrder>> BrowseByTechnicianAsync(Guid technicianId)
    {
        return await context.WorkOrderAssignees
            .Where(a => a.TechnicianId == technicianId)
            .Join(context.WorkOrders, a => a.WorkOrderId, wo => wo.Id, (a, wo) => wo)
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<WorkOrder>> BrowseAllAsync()
    {
        return await context.WorkOrders
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync();
    }

    public async Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseByOperatorAsync(Guid operatorId, WorkOrderFilterDto filter)
    {
        var (page, pageSize) = NormalizePaging(filter.Page, filter.PageSize);
        var query = ApplyFilter(context.WorkOrders.Where(wo => wo.OperatorId == operatorId), filter);
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseByTechnicianAsync(Guid technicianId, WorkOrderFilterDto filter)
    {
        var (page, pageSize) = NormalizePaging(filter.Page, filter.PageSize);
        var assignedOrderIds = context.WorkOrderAssignees
            .Where(a => a.TechnicianId == technicianId)
            .Select(a => a.WorkOrderId);
        var query = ApplyFilter(
            context.WorkOrders.Where(wo => assignedOrderIds.Contains(wo.Id)),
            filter);
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<(IReadOnlyList<WorkOrder> Items, int TotalCount)> BrowseAllAsync(WorkOrderFilterDto filter)
    {
        var (page, pageSize) = NormalizePaging(filter.Page, filter.PageSize);
        var query = ApplyFilter(context.WorkOrders, filter);
        var totalCount = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, totalCount);
    }

    public async Task<bool> HasReportsAsync(Guid workOrderId)
    {
        var count = await context.Database
            .SqlQueryRaw<long>("SELECT COUNT(*) AS \"Value\" FROM reports.\"Reports\" WHERE \"WorkOrderId\" = {0}", workOrderId)
            .SingleAsync();
        return count > 0;
    }

    public Task DeleteAsync(WorkOrder workOrder)
    {
        context.WorkOrders.Remove(workOrder);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<WorkOrderAssignee>> GetAssigneesAsync(Guid workOrderId)
    {
        return await context.WorkOrderAssignees
            .Where(a => a.WorkOrderId == workOrderId)
            .OrderBy(a => a.AssignedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<WorkOrderAssignee>> GetAssigneesForOrdersAsync(IEnumerable<Guid> workOrderIds)
    {
        return await context.WorkOrderAssignees
            .Where(a => workOrderIds.Contains(a.WorkOrderId))
            .OrderBy(a => a.AssignedAt)
            .ToListAsync();
    }

    public Task AddAssigneeAsync(Guid workOrderId, Guid technicianId, DateTime assignedAt)
    {
        var assignee = WorkOrderAssignee.Create(workOrderId, technicianId, assignedAt);
        context.WorkOrderAssignees.Add(assignee);
        return Task.CompletedTask;
    }

    public async Task RemoveAssigneeAsync(Guid workOrderId, Guid technicianId)
    {
        var assignee = await context.WorkOrderAssignees
            .SingleOrDefaultAsync(a => a.WorkOrderId == workOrderId && a.TechnicianId == technicianId);

        if (assignee is not null)
        {
            context.WorkOrderAssignees.Remove(assignee);
        }
    }

    public async Task<bool> IsAssignedAsync(Guid workOrderId, Guid technicianId)
    {
        return await context.WorkOrderAssignees
            .AnyAsync(a => a.WorkOrderId == workOrderId && a.TechnicianId == technicianId);
    }

    private static IQueryable<WorkOrder> ApplyFilter(IQueryable<WorkOrder> query, WorkOrderFilterDto filter)
    {
        if (!string.IsNullOrWhiteSpace(filter.Status))
        {
            if (!WorkOrderStatus.IsValid(filter.Status))
                throw new Exceptions.InvalidWorkOrderFilterException(filter.Status);

            query = query.Where(wo => wo.Status == filter.Status);
        }

        if (!string.IsNullOrWhiteSpace(filter.Priority))
        {
            if (!WorkOrderPriority.IsValid(filter.Priority))
                throw new Exceptions.InvalidWorkOrderFilterException(filter.Priority);

            query = query.Where(wo => wo.Priority == filter.Priority);
        }

        if (filter.DateFrom.HasValue)
            query = query.Where(wo => wo.Deadline >= filter.DateFrom.Value);

        if (filter.DateTo.HasValue)
            query = query.Where(wo => wo.Deadline <= filter.DateTo.Value);

        return query.OrderByDescending(wo => wo.CreatedAt);
    }

    private static (int Page, int PageSize) NormalizePaging(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize < 1 ? 20 : pageSize > 500 ? 500 : pageSize;
        return (safePage, safePageSize);
    }
}
