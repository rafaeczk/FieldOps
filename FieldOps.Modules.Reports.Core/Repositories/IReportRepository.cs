using FieldOps.Modules.Reports.Core.Entities;

namespace FieldOps.Modules.Reports.Core.Repositories;

internal interface IReportRepository
{
    Task CreateAsync(Report report);
    Task<Report?> GetAsync(Guid id);
    Task<IReadOnlyList<Report>> BrowseByWorkOrderAsync(Guid workOrderId);
    Task<IReadOnlyList<Report>> BrowseByTechnicianAsync(Guid technicianId);
    Task<IReadOnlyList<Report>> BrowsePendingSyncAsync(Guid technicianId);
    Task DeleteAsync(Report report);
}
