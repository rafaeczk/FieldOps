using FieldOps.Modules.Reports.Core.Entities;
using FieldOps.Modules.Reports.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Core.DAL.Repositories;

internal class ReportRepository(ReportDbContext context) : IReportRepository
{
    private readonly ReportDbContext context = context;

    public Task CreateAsync(Report report)
    {
        context.Reports.Add(report);
        return Task.CompletedTask;
    }

    public async Task<Report?> GetAsync(Guid id)
    {
        return await context.Reports.SingleOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IReadOnlyList<Report>> BrowseByWorkOrderAsync(Guid workOrderId)
    {
        return await context.Reports
            .Where(r => r.WorkOrderId == workOrderId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Report>> BrowseByTechnicianAsync(Guid technicianId)
    {
        return await context.Reports
            .Where(r => r.TechnicianId == technicianId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Report>> BrowsePendingSyncAsync(Guid technicianId)
    {
        return await context.Reports
            .Where(r => r.TechnicianId == technicianId && r.Status == "PENDING")
            .OrderBy(r => r.CreatedAt)
            .ToListAsync();
    }

    public Task DeleteAsync(Report report)
    {
        context.Reports.Remove(report);
        return Task.CompletedTask;
    }
}
