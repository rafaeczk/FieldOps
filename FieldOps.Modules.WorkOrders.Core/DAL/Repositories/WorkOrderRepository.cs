using FieldOps.Modules.WorkOrders.Core.Entities;
using FieldOps.Modules.WorkOrders.Core.Repositories;
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
        return await context.WorkOrders
            .Where(wo => wo.TechnicianId == technicianId)
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<WorkOrder>> BrowseAllAsync()
    {
        return await context.WorkOrders
            .OrderByDescending(wo => wo.CreatedAt)
            .ToListAsync();
    }

    public async Task<bool> HasReportsAsync(Guid workOrderId)
    {
        var count = await context.Database
            .SqlQueryRaw<int>("SELECT COUNT(*) AS \"Value\" FROM reports.\"Reports\" WHERE \"WorkOrderId\" = {0}", workOrderId)
            .SingleAsync();
        return count > 0;
    }

    public Task DeleteAsync(WorkOrder workOrder)
    {
        context.WorkOrders.Remove(workOrder);
        return Task.CompletedTask;
    }
}
