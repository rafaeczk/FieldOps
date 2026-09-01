using FieldOps.Modules.Reports.Application.Common;

namespace FieldOps.Modules.Reports.Infrastructure.EF.Repositories;

internal class ReportsUnitOfWork(ReportsDbContext context) : IReportsUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
