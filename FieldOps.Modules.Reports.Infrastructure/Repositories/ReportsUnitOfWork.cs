using FieldOps.Modules.Reports.Application.Common;
using FieldOps.Shared.Abstractions.Errors;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Infrastructure.EF.Repositories;

internal class ReportsUnitOfWork(ReportsDbContext context) : IReportsUnitOfWork
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
