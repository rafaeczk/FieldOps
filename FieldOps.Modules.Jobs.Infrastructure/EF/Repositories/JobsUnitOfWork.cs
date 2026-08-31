using FieldOps.Modules.Jobs.Application.Common;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal class JobsUnitOfWork(JobsDbContext context) : IJobsUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
