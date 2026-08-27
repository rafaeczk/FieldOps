using FieldOps.Modules.Reports.Core.Repositories;
using FieldOps.Shared.Infrastructure.Postgres;

namespace FieldOps.Modules.Reports.Core.DAL;

internal class ReportUnitOfWork(ReportDbContext context) : IReportUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
