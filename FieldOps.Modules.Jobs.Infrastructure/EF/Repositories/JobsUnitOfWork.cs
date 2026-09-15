using FieldOps.Modules.Jobs.Application.Common;
using FieldOps.Shared.Abstractions.Errors;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal class JobsUnitOfWork(JobsDbContext context) : IJobsUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        try
        {
            return await context.SaveChangesAsync(ct);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new ConcurrencyException();
        }
    }
}
