using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Modules.Reports.Domain.Reports.Repositories;

namespace FieldOps.Modules.Reports.Infrastructure.EF.Repositories;

internal sealed class ReportsWriteRepository(ReportsDbContext context) : IReportsWriteRepository
{
    private readonly ReportsDbContext context = context;

    public void Add(Report report)
    {
        context.Reports.Add(report);
    }

    public void Update(Report report)
    {
        context.Reports.Update(report);
    }
}
